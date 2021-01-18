using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate;
using Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate;
using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate;
using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;

namespace Senai.Vagas.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscricoesController : ControllerBase
    {
        public IInscricaoQueries _inscricoesQueries { get; set; }
        public IVagaQueries _vagaQueries { get; set; }
        public IVagaRepository _vagaRepository { get; set; }
        public IStatusVagaRepository _statusVagaRepository { get; set; }
        public IUsuarioRepository _usuarioRepository { get; set; }
        public IUsuarioEmpresaRepository _usuarioEmpresaRepository { get; set; }
        public IEnderecoRepository _enderecoRepository { get; set; }
        public IInscricaoRepository _inscricaoRepository { get; set; }
        public IUsuarioCandidatoAlunoRepository _usuarioCandidatoAlunoRepository { get; set; }
        public IAlunoRepository _alunoRepository { get; set; }
        public IConviteEntrevistaRepository _conviteEntrevistaRepository { get; set; }

        public InscricoesController(IInscricaoQueries inscricoesQueries, IVagaQueries vagaQueries, IVagaRepository vagaRepository, IStatusVagaRepository statusVagaRepository, IUsuarioRepository usuarioRepository, IUsuarioEmpresaRepository usuarioEmpresaRepository, IEnderecoRepository enderecoRepository, IInscricaoRepository inscricaoRepository, IUsuarioCandidatoAlunoRepository usuarioCandidatoAlunoRepository, IAlunoRepository alunoRepository, IConviteEntrevistaRepository conviteEntrevistaRepository)
        {
            _inscricoesQueries = inscricoesQueries;
            _vagaQueries = vagaQueries;
            _vagaRepository = vagaRepository;
            _statusVagaRepository = statusVagaRepository;
            _usuarioRepository = usuarioRepository;
            _usuarioEmpresaRepository = usuarioEmpresaRepository;
            _enderecoRepository = enderecoRepository;
            _inscricaoRepository = inscricaoRepository;
            _usuarioCandidatoAlunoRepository = usuarioCandidatoAlunoRepository;
            _alunoRepository = alunoRepository;
            _conviteEntrevistaRepository = conviteEntrevistaRepository;
        }



        /// <summary>
        /// Buscar as incrições de uma vaga específica 
        /// </summary>
        /// <param name="vagaId"></param>
        /// <returns></returns>
        [HttpGet ("vaga/{vagaId}")]
        public IActionResult GetAllInscricoesByVagaId(long vagaId)
        {
            List<InscricaoViewModel> inscricoes = new List<InscricaoViewModel>();
            try
            {
                inscricoes = _inscricoesQueries.GetAllInscricoesByVagaId(vagaId);

                if (inscricoes == null)
                    return StatusCode(404, $"Não foi encontrado uma vaga com id [{vagaId}]");
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao buscar todas as inscricoes cadastradas na vaga.");
            }

            return StatusCode(200, inscricoes);
        }

        /// <summary>
        /// Buscar as incrições de um candidato (empresa)
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("usuario/{usuarioId}")]
        public IActionResult GetAllVagaInscricoesByUsuarioId(long usuarioId)
        {
            List<VagaViewModel> vagasInscritas = new List<VagaViewModel>();
            try
            {
                // Busca usuário por Id
                var usuarioDb = _usuarioRepository.GetById(usuarioId);

                // Caso não existir
                if (usuarioDb == null)
                    return StatusCode(404, $"Este Id de usuário [{usuarioId}] não existe.");

                // Busca o vinculo entre usuario X usuarioCandidato
                var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.GetByUsuarioId(usuarioDb.Id);

                // Caso não achar, o usuário não é um candidato
                if (usuarioCandidatoDb == null)
                    return StatusCode(401, "Este usuário não pode ver suas inscricoes");

                // Busca inscrições de um usuário candidato específico
                vagasInscritas = _inscricoesQueries.GetAllVagaInscricoesByCandidatoId(usuarioCandidatoDb.Id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Houve um erro interno ao buscar todas as inscricoes de um candidato específico.");
            }

            return StatusCode(200, vagasInscritas);
        }

        /// <summary>
        /// Increver em uma vaga (candidato)
        /// </summary>
        /// <param name="vagaId"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpPost("vaga/{vagaId}/usuario/{usuarioId}")]
        public async Task<IActionResult> CandidatoInscreverOuCancelarInscricaoVaga(long vagaId, long usuarioId)
        {
            // Busca usuario por id
            var usuarioDb = _usuarioRepository.GetById(usuarioId);

            // Caso não encontrar
            if (usuarioDb == null)
                return StatusCode(400, $"Este Id de usuário [{usuarioId}] não existe");

            // Busca usuarioCandidatoAluno por id
            var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.GetByUsuarioId(usuarioDb.Id);

            // Caso não encontrar
            if (usuarioCandidatoDb == null)
                return StatusCode(400, "Este usuário não tem um perfil de candidato.");

            // Busca vaga por id
            var vagaDb = _vagaRepository.GetById(vagaId);

            // Caso não encontrar
            if (vagaDb == null)
                return StatusCode(400, $"A vaga de id [{vagaId}] não existe no banco de dados.");

            // Busca statusVaga ativa no BD
            var statusVagaAtivaDb = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaAtiva));

            // Caso a vaga ja tenha sido encerrada ou excluída
            if (vagaDb.StatusVagaId != statusVagaAtivaDb.Id)
                return StatusCode(400, "Esta vaga já foi encerrada ou excluída.");

            // Busca inscrição pelo ID da Vaga e Id do UsuarioCandidatoAluno
            var inscricaoDb = _inscricaoRepository.GetInscricaoByVagaIdAndCandidatoId(vagaDb.Id, usuarioCandidatoDb.Id);

            // Caso não encontrar
            if (inscricaoDb == null)
            {
                // Cria uma nova inscrição referenciando o candidato com a vaga que se inscreveu
                Inscricao inscricao = null;
                try
                {
                    inscricao = new Inscricao(usuarioCandidatoDb.Id, vagaDb.Id);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Houve um erro interno ao criar uma nova inscrição.");
                }

                // Cria a inscrição na DB
                _inscricaoRepository.Create(inscricao);
            }
            else
            {
                // Caso achar uma inscrição, quer dizer que o usuário quer cancelar sua inscrição, ou mesmo, inscrever-se novamente na vaga
                if (inscricaoDb.Ativo == true)
                    inscricaoDb.AlterarParaAntigo();
                else
                    inscricaoDb.AlterarParaAtual();
                // Altera a inscrição na DB
                _inscricaoRepository.UpdateInscricao(inscricaoDb);
            }

            // Salva qualquer alteração no banco de dados
            await _inscricaoRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "Usuário se inscreveu ou cancelou sua incrição com sucesso.");
        }
    }
}
