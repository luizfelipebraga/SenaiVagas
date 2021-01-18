using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Vagas.Backend.Application.DTOs.Inputs;
using Senai.Vagas.Backend.Application.ResponseViewModels;
using Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate;
using Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;

namespace Senai.Vagas.Backend.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ConvitesEntrevistasController : ControllerBase
    {
        private IConviteEntrevistaRepository _conviteEntrevistaRepository { get; set; }
        private IEnderecoRepository _enderecoRepository { get; set; }
        private IVagaRepository _vagaRepository { get; set; }
        private IUsuarioRepository _usuarioRepository { get; set; }
        private IUsuarioCandidatoAlunoRepository _usuarioCandidatoAlunoRepository { get; set; }
        private IEmpresaRepository _empresaRepository { get; set; }
        private IInscricaoRepository _inscricaoRepository { get; set; }
        private IUsuarioEmpresaRepository _usuarioEmpresaRepository { get; set; }

        public ConvitesEntrevistasController(IConviteEntrevistaRepository conviteEntrevistaRepository, IEnderecoRepository enderecoRepository, IVagaRepository vagaRepository, IUsuarioRepository usuarioRepository, IUsuarioCandidatoAlunoRepository usuarioCandidatoAlunoRepository, IEmpresaRepository empresaRepository, IInscricaoRepository inscricaoRepository, IUsuarioEmpresaRepository usuarioEmpresaRepository)
        {
            _conviteEntrevistaRepository = conviteEntrevistaRepository;
            _enderecoRepository = enderecoRepository;
            _vagaRepository = vagaRepository;
            _usuarioRepository = usuarioRepository;
            _usuarioCandidatoAlunoRepository = usuarioCandidatoAlunoRepository;
            _empresaRepository = empresaRepository;
            _inscricaoRepository = inscricaoRepository;
            _usuarioEmpresaRepository = usuarioEmpresaRepository;
        }

        /// <summary>
        /// Buscar convite de entrevista (candidato/empresa)
        /// </summary>
        /// <param name="vagaId"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("convites/vaga/{vagaId}/usuario/{usuarioId}")]
        public IActionResult GetConviteByVagaId(long vagaId, long usuarioId)
        {
            Usuario usuario = _usuarioRepository.GetById(usuarioId);

            // Caso não encontre
            if (usuario == null)
                return StatusCode(404, "Usuário não encontrado");

            // Busca usuarioCandidato por UsuarioId
            var UsuarioCandidato = _usuarioCandidatoAlunoRepository.GetByUsuarioId(usuario.Id);

            // Caso não encontre
            if (UsuarioCandidato == null)
                return StatusCode(404, "Este usuário não é um candidato para receber convites.");

            // Busca vaga por Id
            Vaga vaga = _vagaRepository.GetById(vagaId);

            // Caso não encontre
            if (vaga == null)
                return StatusCode(404, "Vaga não encontrada");

            // Busca usuarioEmpresa por usuarioEmpresaId
            var usuarioEmpresa = _usuarioEmpresaRepository.GetById(vaga.UsuarioEmpresaId);

            // Caso não encontre
            if (usuarioEmpresa == null)
                return StatusCode(404, "Este usuário não tem um perfil de empresa.");

            // Busca empresa por Id e retorna apenas o nome da mesma
            var NomeEmpresa = _empresaRepository.GetById(usuarioEmpresa.EmpresaId).Nome;

            // Busca convite por vagaId e UsuarioId
            ConviteEntrevista convite = _conviteEntrevistaRepository.GetConviteEntrevistasByVagaIdAndCandidatoId(vagaId, UsuarioCandidato.Id);

            // Caso não encontre
            if (convite == null)
                return StatusCode(404, "Convite não encontrado");

            //Busca endereço da vaga
            Endereco endereco = _enderecoRepository.GetById(convite.EnderecoId);

            //Busca município da vaga
            Municipio municipio = _enderecoRepository.GetMunicipioById(endereco.MunicipioId);

            //Busca UfSigla da vaga
            UfSigla ufSigla = _enderecoRepository.GetUfSiglaById(municipio.UfSiglaId);

            // Cria o ViewModel de convite entrevista "diferente"
            ConviteEntrevistaViewModel conviteEntrevistaViewModel = null;
            try
            {
                conviteEntrevistaViewModel = new ConviteEntrevistaViewModel(convite.Id, NomeEmpresa, convite.DataHoraEntrevista, endereco.Logradouro, endereco.Bairro, endereco.Numero, municipio.Descricao, ufSigla.UFSigla, convite.InfosComplementares);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar um novo endereco");
            }

            return StatusCode(200, conviteEntrevistaViewModel);
        }

        /// <summary>
        /// Criar convite de entrevista (empresa)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="vagaId"></param>
        /// <param name="usuarioCandidatoId"></param>
        /// <returns></returns>
        [HttpPost("cadastrar/vaga/{vagaId}/usuario-candidato/{usuarioCandidatoId}")]
        public async Task<IActionResult> CadastrarConvite(CriarConviteInput input, long vagaId, long usuarioCandidatoId)
        {
            // Busca usuarioCandidato por Id
            var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.GetById(usuarioCandidatoId);

            // Caso não encontrar
            if (usuarioCandidatoDb == null)
                return StatusCode(400, "Este usuário não é um candidato.");

            // Busca vaga por Id
            var vagaDb = _vagaRepository.GetById(vagaId);

            // Caso não encontre
            if (vagaDb == null)
                return StatusCode(400, "Esta vaga não existe");

            // Busca Inscricao por vagaId e UsuarioCandidatoAlunoId
            var inscricaoDb = _inscricaoRepository.GetInscricaoByVagaIdAndCandidatoIdAtual(vagaId, usuarioCandidatoDb.Id);

            // Caso não encontre
            if (inscricaoDb == null)
                return StatusCode(400, "Não existe uma inscrição.");

            // Busca Municipio por Id
            var municipioDb = _enderecoRepository.GetMunicipioById(input.Municipio.Id);

            // Caso não encontre
            if (municipioDb == null)
                return StatusCode(404, $"O município de [{input.Municipio.Descricao}] não existe no banco de dados.");

            try
            {
                // Altera para convite recebido na entidade Inscricao
                inscricaoDb.AlterarConviteRecebido();

                // Altera no BD
                _inscricaoRepository.UpdateInscricao(inscricaoDb);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao alterar a inscrição específica.");
            }

            // Cria endereço da entrevista
            Endereco endereco = null;
            try
            {
                endereco = new Endereco(input.Bairro, input.Rua, input.Numero, input.Municipio.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar um novo endereco");
            }

            // Cria no BD
            endereco = _enderecoRepository.Create(endereco);

            // Cria convite de entrevista
            ConviteEntrevista convite = null;
            try
            {
                convite = new ConviteEntrevista(input.DataHoraEntrevista, input.infosComplementares, endereco.Id, usuarioCandidatoDb.Id, vagaDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar um novo convite.");
            }

            // Cria no BD
            _conviteEntrevistaRepository.Create(convite);

            //TODO: Notificar candidato que recebeu um convite de entrevista pelo EMAIL

            // Salva alterações no BD
            await _conviteEntrevistaRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "O Convite foi criado com sucesso!");
        }
    }
}
