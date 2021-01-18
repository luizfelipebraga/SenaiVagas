using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;
using Senai.Vagas.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "3")]
    public class AdministradorController : ControllerBase
    {
        public IUsuarioRepository _usuarioRepository { get; set; }
        public ITipoUsuarioRepository _tipoUsuarioRepository { get; set; }
        public IHistoricoStatusUsuarioRepository _historicoStatusUsuarioRepository { get; set; }
        public IStatusUsuarioRepository _statusUsuarioRepository { get; set; }
        public IVagaRepository _vagaRepository { get; set; }
        public IStatusVagaRepository _statusVagaRepository { get; set; }
        public IInscricaoRepository _inscricaoRepository { get; set; }
        public IUsuarioCandidatoAlunoRepository _usuarioCandidatoAlunoRepository { get; set; }
        public IUsuarioAdministradorRepository _usuarioAdministradorRepository { get; set; }
        public IAdministradorQueries _administradorQueries { get; set; }

        public AdministradorController(IUsuarioRepository usuarioRepository, ITipoUsuarioRepository tipoUsuarioRepository, IHistoricoStatusUsuarioRepository historicoStatusUsuarioRepository, IStatusUsuarioRepository statusUsuarioRepository, IVagaRepository vagaRepository, IStatusVagaRepository statusVagaRepository, IInscricaoRepository inscricaoRepository, IUsuarioCandidatoAlunoRepository usuarioCandidatoAlunoRepository, IUsuarioAdministradorRepository usuarioAdministradorRepository, IAdministradorQueries administradorQueries)
        {
            _usuarioRepository = usuarioRepository;
            _tipoUsuarioRepository = tipoUsuarioRepository;
            _historicoStatusUsuarioRepository = historicoStatusUsuarioRepository;
            _statusUsuarioRepository = statusUsuarioRepository;
            _vagaRepository = vagaRepository;
            _statusVagaRepository = statusVagaRepository;
            _inscricaoRepository = inscricaoRepository;
            _usuarioCandidatoAlunoRepository = usuarioCandidatoAlunoRepository;
            _usuarioAdministradorRepository = usuarioAdministradorRepository;
            _administradorQueries = administradorQueries;
        }

        /// <summary>
        /// Retornar todos os administradores da plataforma
        /// </summary>
        /// <returns></returns>
        [HttpGet("buscar-todos")]
        public IActionResult AdminGetAllAdmins()
        {
            try
            {
                var admins = _administradorQueries.GetAllAdministradores();

                return StatusCode(200, admins);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao tentar retornar as informações de todos os administradores.");
            }
        }

        /// <summary>
        /// Desativar usuário (admin)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpPut("desativar/usuario/{email}/admin-atuante/{usuarioId}")]
        public async Task<IActionResult> PutDesativarUsuario(string email, long usuarioId)
        {
            // Busca o adm pelo usuarioId
            var admDb = _usuarioRepository.GetById(usuarioId);

            // Caso não encontre
            if (admDb == null)
                return StatusCode(404, $"Este usuário de id [{usuarioId}] não existe.");

            // Busca o vinculo entre Usuario X Administrador
            var usuarioAdm = _usuarioAdministradorRepository.GetByUsuarioId(admDb.Id);

            // Caso não encontre, não é um perfil de administrador
            if (usuarioAdm == null)
                return StatusCode(401, "Este usuário não tem um perfil de administrador para desativar um usuário.");

            // Busca um usuário por email
            var usuarioBuscado = _usuarioRepository.GetUsuarioByEmail(email);

            // Caso não encontre
            if (usuarioBuscado == null)
                return StatusCode(404, "Usuário não encontrado!");

            // Caso o administrador tente desativar seu próprio usuário
            if (usuarioBuscado.Email == admDb.Email)
                return StatusCode(403, "Você não pode excluir seu próprio usuário.");

            // Caso o adminisrador tente desativar o usuário padrão da plataforma
            if (usuarioBuscado.Email == "admin@email.com")
                return StatusCode(403, "Você não pode excluir o administrador padrão.");

            // Busca o historico do usuário
            var historicoBuscado = _historicoStatusUsuarioRepository.GetHistoricoAtualByUsuarioId(usuarioBuscado.Id);
      
            // Caso não encontre
            if (historicoBuscado == null)
                return StatusCode(404, "Histórico de Status do usuário não encontrado!");

            // Pega o status padrão "Conta Desativado"
            var StatusUsuario = _statusUsuarioRepository.GetByDescricao(StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaDesativada));

            // Pega o status padrão "Conta ativa"
            var StatusUsuarioAtivo = _statusUsuarioRepository.GetByDescricao(StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaAtiva));

            // Caso a conta não estja ativa
            if (historicoBuscado.StatusUsuarioId != StatusUsuarioAtivo.Id)
                return StatusCode(404, "Este usuário ja está desativado!");

            // Altera o atual para antigo e cria o novo historico 
            HistoricoStatusUsuario historicoStatusUsuario = null;
            try
            {
                historicoBuscado.AlterarParaAntigo();
                _historicoStatusUsuarioRepository.PutAlterarHistorico(historicoBuscado);

                historicoStatusUsuario = new HistoricoStatusUsuario(StatusUsuario.Id, usuarioBuscado.Id);
                _historicoStatusUsuarioRepository.Create(historicoStatusUsuario);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar o histórico");
            }

            // Salva alterações no BD
            await _historicoStatusUsuarioRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "Usuário desativado!");
        }

        /// <summary>
        /// Excluir vaga (admin)
        /// </summary>
        /// <param name="vagaId"></param>
        /// <returns></returns>
        [HttpPut("excluir/vaga/{vagaId}")]
        public async Task<IActionResult> PutExcluirVaga(long vagaId)
        {
            var vagaBuscada = _vagaRepository.GetById(vagaId);

            if (vagaBuscada == null)
                return StatusCode(404, "Vaga não encontrada!");

            // Busca statusVaga ativa no BD
            var statusVagaExcluidaDb = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaExcluida));

            // Verifica se o status da vaga é diferente de vaga ativa, caso for, retorna que a vaga já esta desativada (ou excluída)
            if (vagaBuscada.StatusVagaId == statusVagaExcluidaDb.Id)
                return StatusCode(200, $"Esta vaga id [{vagaBuscada.Id}] já esta excluída.");

            try
            {
                var statusVaga = _statusVagaRepository.GetByDescricao(StatusVagaDefaultValuesAcess.GetValue(StatusVagaDefaultValues.VagaExcluida));

                vagaBuscada.AlterarStatusVaga(statusVaga.Id);
                _vagaRepository.UpdateVaga(vagaBuscada);
            }
            catch (Exception)
            {
                return StatusCode(500, "Erro ao excluir a vaga!");
            }

            await _statusVagaRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "Vaga excluída com sucesso!");
        }

        /// <summary>
        /// Remover inscrição (admin)
        /// </summary>
        /// <param name="inscricaoId"></param>
        /// <returns></returns>
        [HttpPut("remover/inscricao/{inscricaoId}")]
        public async Task<IActionResult> PutRemoverInscricao(long inscricaoId)
        {
            var inscricao = _inscricaoRepository.GetById(inscricaoId);

            if (inscricao == null)
                return StatusCode(404, "Inscrição nao encontrada");

            try
            {
                inscricao.AlterarParaAntigo();
                _inscricaoRepository.UpdateInscricao(inscricao);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao remover a inscrição");
            }

            await _inscricaoRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "Inscrição removida com sucesso!");
        }
    }
}
