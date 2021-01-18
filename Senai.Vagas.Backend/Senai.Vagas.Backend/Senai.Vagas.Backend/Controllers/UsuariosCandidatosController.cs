using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Vagas.Backend.Application.DTOs.Inputs;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.AreaAggregate;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;

namespace Senai.Vagas.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosCandidatosController : ControllerBase
    {
        public IUsuarioQueries _usuarioQueries { get; set; }
        public IVagaRepository _vagaRepository { get; set; }
        public IUsuarioRepository _usuarioRepository { get; set; }
        public IStatusUsuarioRepository _statusUsuarioRepository { get; set; }
        public IHistoricoStatusUsuarioRepository _historicoStatusUsuarioRepository { get; set; }
        public ITipoUsuarioRepository _tipoUsuarioRepository { get; set; }
        public IEnderecoRepository _enderecoRepository { get; set; }
        public IAreaRepository _areaRepository { get; set; }
        public IUsuarioCandidatoAlunoRepository _usuarioCandidatoAlunoRepository { get; set; }
        public IAlunoRepository _alunoRepository { get; set; }

        public UsuariosCandidatosController(IUsuarioQueries usuarioQueries, IVagaRepository vagaRepository, IUsuarioRepository usuarioRepository, IStatusUsuarioRepository statusUsuarioRepository, IHistoricoStatusUsuarioRepository historicoStatusUsuarioRepository, ITipoUsuarioRepository tipoUsuarioRepository, IEnderecoRepository enderecoRepository, IAreaRepository areaRepository, IUsuarioCandidatoAlunoRepository usuarioCandidatoAlunoRepository, IAlunoRepository alunoRepository)
        {
            _usuarioQueries = usuarioQueries;
            _vagaRepository = vagaRepository;
            _usuarioRepository = usuarioRepository;
            _statusUsuarioRepository = statusUsuarioRepository;
            _historicoStatusUsuarioRepository = historicoStatusUsuarioRepository;
            _tipoUsuarioRepository = tipoUsuarioRepository;
            _enderecoRepository = enderecoRepository;
            _areaRepository = areaRepository;
            _usuarioCandidatoAlunoRepository = usuarioCandidatoAlunoRepository;
            _alunoRepository = alunoRepository;
        }

        [HttpGet("area-interesse/usuario/{usuarioId}")]
        public IActionResult GetAllAreasInteresseByCandidatoId(long usuarioId)
        {
            try
            {
                Usuario usuario = _usuarioRepository.GetById(usuarioId);

                // Caso não encontre
                if (usuario == null)
                    return StatusCode(404, "Usuário não encontrado");

                // Busca usuarioCandidato por UsuarioId
                var UsuarioCandidato = _usuarioCandidatoAlunoRepository.GetByUsuarioId(usuario.Id);

                // Caso não encontre
                if (UsuarioCandidato == null)
                    return StatusCode(404, "Este usuário não é um candidato.");

                var usuarioCandidato = _usuarioQueries.GetAllAreasInteresseCandidatoByCandidatoId(UsuarioCandidato.Id);
                return StatusCode(200, usuarioCandidato);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao tentar retornar as áreas de interesse do candidato");
            }
        }


        /// <summary>
        /// Buscar todos os alunos (admin)
        /// </summary>
        /// <returns></returns>
        [HttpGet("buscar-todos")]
        [Authorize(Roles = "3")]
        public IActionResult AdminGetAllAlunos()
        {
            try
            {
                var alunos = _usuarioQueries.GetAllAlunos();

                return StatusCode(200, alunos);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao tentar retornar as informações de todos os candidatos.");
            }
        }

        /// <summary>
        /// Buscar o perfil de um aluno específico (admin)
        /// </summary>
        /// <param name="alunoId"></param>
        /// <returns></returns>
        [HttpGet("perfil/aluno/{alunoId}")]
        [Authorize(Roles = "3")]
        public IActionResult AdminGetAllInformacoesCandidatoByAlunoId(long alunoId)
        {
            try
            {
                // Busca aluno específico
                var aluno = _usuarioQueries.GetInformacoesPerfilAlunoByAlunoId(alunoId);

                // Caso não encontre
                if (aluno == null)
                    return StatusCode(404, "Este aluno não existe.");

                // Busca vinculo de aluno X usuário (candidato)
                var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.GetByAlunoId(alunoId);

                // Verifica se usuário ainda não foi cadastrado (só mostra infos do aluno), ou se usuário foi excluído
                if (usuarioCandidatoDb != null)
                {
                    var historicoUsuario = _historicoStatusUsuarioRepository.GetHistoricoAtualByUsuarioId(usuarioCandidatoDb.UsuarioId);

                    var statusUsuarioExcluidoDb = _statusUsuarioRepository.GetByDescricao(StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaExcluida));

                    if (historicoUsuario.StatusUsuarioId == statusUsuarioExcluidoDb.Id)
                    {
                        aluno.PerfilCandidato.SobreOCandidato = "USUÁRIO EXCLUÍDO.";
                    }
                }
                else
                {
                    aluno.PerfilCandidato.SobreOCandidato = "USUÁRIO NÃO CADASTRADO.";
                }

                return StatusCode(200, aluno);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao tentar retornar as informações de um candidato específico.");
            }
        }

        /// <summary>
        /// Buscar o perfil de um aluno específico por candidatoId (empresa)
        /// </summary>
        /// <param name="usuarioCandidatoId"></param>
        /// <returns></returns>
        [HttpGet("perfil/usuarioCandidato/{usuarioCandidatoId}")]
        public IActionResult AdminGetAllInformacoesCandidatoByCandidatoId(long usuarioCandidatoId)
        {
            try
            {
                // Busca usuarioCandidato por Id
                var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.GetById(usuarioCandidatoId);

                // Caso não encontrar
                if (usuarioCandidatoDb == null)
                    return StatusCode(400, "Este usuário não é um candidato.");

                // Busca aluno específico
                var aluno = _usuarioQueries.GetInformacoesPerfilAlunoByAlunoId(usuarioCandidatoDb.AlunoId);

                // Caso não encontre
                if (aluno == null)
                    return StatusCode(404, "Este aluno não existe.");

                // Verifica se usuário ainda não foi cadastrado (só mostra infos do aluno), ou se usuário foi excluído
                if (usuarioCandidatoDb != null)
                {
                    var historicoUsuario = _historicoStatusUsuarioRepository.GetHistoricoAtualByUsuarioId(usuarioCandidatoDb.UsuarioId);

                    var statusUsuarioExcluidoDb = _statusUsuarioRepository.GetByDescricao(StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaExcluida));

                    if (historicoUsuario.StatusUsuarioId == statusUsuarioExcluidoDb.Id)
                    {
                        aluno.PerfilCandidato.SobreOCandidato = "USUÁRIO EXCLUÍDO.";
                    }
                }
                else
                {
                    aluno.PerfilCandidato.SobreOCandidato = "USUÁRIO NÃO CADASTRADO.";
                }

                return StatusCode(200, aluno);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao tentar retornar as informações de um candidato específico.");
            }
        }

        /// <summary>
        /// Buscar o perfil de um usuário específico
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("perfil/usuario/{usuarioId}")]
        public IActionResult GetAllInformacoesCandidatoByUsuarioId(long usuarioId)
        {
            try
            {
                var usuarioDb = _usuarioRepository.GetById(usuarioId);

                if (usuarioDb == null)
                    return StatusCode(400, $"Este Id de usuário [{usuarioId}] não existe");

                var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.GetByUsuarioId(usuarioDb.Id);

                if (usuarioCandidatoDb == null)
                    return StatusCode(401, "Este usuário não tem um perfil de candidato.");

                var aluno = _usuarioQueries.GetInformacoesPerfilAlunoByAlunoId(usuarioCandidatoDb.AlunoId);

                if (aluno == null)
                    return StatusCode(404, "Este aluno não existe.");

                return StatusCode(200, aluno);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao tentar retornar as informações de um candidato específico.");
            }
        }

        /// <summary>
        ///  Alterar descrição do usuário (candidato)
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("alterar/descricao/link/{usuarioId}")]
        public async Task<IActionResult> PutAlterarDescricaoCandidatoAluno(long usuarioId, PerfilUsuarioCandidatoInput input)
        {
            var usuario = _usuarioRepository.GetById(usuarioId);
            if (usuario == null)
                return StatusCode(404, "Esse perfil de usuário não existe!");

            var usuarioCandidato = _usuarioCandidatoAlunoRepository.GetByUsuarioId(usuarioId);
            if (usuarioCandidato == null)
                return StatusCode(404, "Esse perfil de candidato não existe!");

            var perfilUsuarioCandidatoAluno = _usuarioCandidatoAlunoRepository.GetPerfilUsuarioCandidatoAlunoByUsuarioCandidatoId(usuarioCandidato.Id);
            if (perfilUsuarioCandidatoAluno == null)
                return StatusCode(404, "Não existe um perfil de usuário padrão no banco de dados.");

            try
            {
                perfilUsuarioCandidatoAluno.AlterarInformacoes(input.LinkExterno, input.SobreOCandidato);
                _usuarioCandidatoAlunoRepository.UpdatePerfilUsuarioCandidatoAluno(perfilUsuarioCandidatoAluno);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao tentar alteras as descrições!");
            }

            await _usuarioCandidatoAlunoRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "Descrições alteradas com sucesso!");
        }

        /// <summary>
        /// Configurar areas de interesse (candidato)
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("usuario/{usuarioId}")]
        public async Task<IActionResult> ConfigurarAreasDeInteresseCandidato(long usuarioId, AreaDeInteresseCandidatoInput input)
        {
            // Busca usuário por Id
            var usuarioDb = _usuarioRepository.GetById(usuarioId);

            // Caso não existir
            if (usuarioDb == null)
                return StatusCode(404, $"Este Id de usuário [{usuarioId}] não existe.");

            var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.GetByUsuarioId(usuarioDb.Id);

            if (usuarioCandidatoDb == null)
                return StatusCode(401, "Este usuário não tem um perfil de candidato para configurar áreas de interesse.");

            var perfisAreasDb = _usuarioCandidatoAlunoRepository.GetAllAreasInteresseByUsuarioCandidatoId(usuarioCandidatoDb.Id);

            List<AreasInteresseCandidatoAluno> perfisAreasNovos = new List<AreasInteresseCandidatoAluno>();

            // Itera entre todas as novas áreas de interesse
            AreasInteresseCandidatoAluno perfilArea = null;
            foreach (var area in input.AreasInteresse)
            {
                // Busca na DB se já existe o perfil Area configurada pelo usuário
                var perfilAreaDb = _usuarioCandidatoAlunoRepository.GetAreasInteresseCandidatoAlunoByAreaIdAndCandidatoId(area.Id, usuarioCandidatoDb.Id);

                // Caso não existir, cria uma nova area de interesse para o usuário
                if (perfilAreaDb == null)
                {
                    try
                    {
                        perfilArea = new AreasInteresseCandidatoAluno(area.Id, usuarioCandidatoDb.Id);

                        perfisAreasNovos.Add(perfilArea);
                    }
                    catch (Exception)
                    {
                        return StatusCode(500, "Houve um erro interno ao criar as áreas recomendadas para a Vaga.");
                    }
                }
                else
                {
                    // Caso existir, quer dizer que o usuário quer altera-lo para ativo novamente, ou apenas, não alterou esta area específica

                    // Verifica se a area de interesse existente esta como "inativa"
                    if (perfilAreaDb.Ativo == false)
                        perfilAreaDb.AlterarParaAtivo();

                    // Altera a perfilAreaUsuarioCandidatoAluno (área de interesse) específica no BD
                    _usuarioCandidatoAlunoRepository.UpdateAreasInteresseCandidatoAluno(perfilAreaDb);
                }
            }

            // Itera entre todas as áreas de interesse do usuario do BD para verificar se usuário removeu alguma área selecionada no input
            foreach (var perfAreaDb in perfisAreasDb)
            {
                // Verifica se alguma área de interesse do BD não existe no input de novas áreas enviado pelo usuário
                if (!input.AreasInteresse.Any(x => x.Id == perfAreaDb.AreaId))
                {
                    // Caso area de interesse do BD NÃO existir no input, significa que o usuário removeu aquela area específica

                    // Altera o PerfilAreaUsuarioCandidatoAluno do BD para "inativo"
                    perfAreaDb.AlterarParaAntigo();

                    // Altera no BD
                    _usuarioCandidatoAlunoRepository.UpdateAreasInteresseCandidatoAluno(perfAreaDb);
                }
            }

            // Caso existir novas áreas de interesse do usuário, adiciona-as no BD
            if (perfisAreasNovos.Any())
            {
                _usuarioCandidatoAlunoRepository.CreateRangeAreasInteresseCandidatoAluno(perfisAreasNovos);
            }

            await _usuarioCandidatoAlunoRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "Áreas de interesse do usuário configuradas com sucesso.");
        }


    }
}
