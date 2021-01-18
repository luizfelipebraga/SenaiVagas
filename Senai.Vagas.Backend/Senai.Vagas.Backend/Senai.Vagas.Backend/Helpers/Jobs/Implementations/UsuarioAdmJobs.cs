using Microsoft.AspNetCore.Identity;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class UsuarioAdmJobs : IJobs
    {
        public IUsuarioRepository _usuarioRepository;
        public IStatusUsuarioRepository _statusUsuarioRepository;
        public IHistoricoStatusUsuarioRepository _historicoStatusUsuarioRepository;
        public ITipoUsuarioRepository _tipoUsuarioRepository;
        public IUsuarioAdministradorRepository _usuarioAdministradorRepository;

        public UsuarioAdmJobs(IUsuarioRepository usuarioRepository, IStatusUsuarioRepository statusUsuarioRepository, IHistoricoStatusUsuarioRepository historicoStatusUsuarioRepository, ITipoUsuarioRepository tipoUsuarioRepository, IUsuarioAdministradorRepository usuarioAdministradorRepository)
        {
            _usuarioRepository = usuarioRepository;
            _statusUsuarioRepository = statusUsuarioRepository;
            _historicoStatusUsuarioRepository = historicoStatusUsuarioRepository;
            _tipoUsuarioRepository = tipoUsuarioRepository;
            _usuarioAdministradorRepository = usuarioAdministradorRepository;
        }

        public async Task ExecuteAsync()
        {
            var usuarioDb = _usuarioRepository.GetUsuarioByEmail("admin@email.com");

            if(usuarioDb == null)
            {
                var tipoUsuarioDb = _tipoUsuarioRepository.GetByDescricao(TipoUsuarioDefaultValuesAccess.GetValue(TipoUsuarioDefaultValues.Administrador));
                var statusUsuarioDb = _statusUsuarioRepository.GetByDescricao(StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaAtiva));
                // Criação do usuário
                Usuario usuario = null;
                try
                {
                    string nome = "Administrador Padrão";
                    string email = "admin@email.com";
                    string senha = "admin12345";

                    // Gera o código HASH da senha
                    var senhaHash = new PasswordHasher<Usuario>().HashPassword(usuario, senha);

                    usuario = new Usuario(nome, email, senhaHash, tipoUsuarioDb.Id);
                }
                catch (Exception)
                {
                }
                
                // Cria o usuário no banco de dados
                var usuarioAdmDb = _usuarioRepository.Create(usuario);

                // Cria o 1° histórico do usuário
                HistoricoStatusUsuario historicoUsuario = null;
                try
                {
                    historicoUsuario = new HistoricoStatusUsuario(statusUsuarioDb.Id, usuarioAdmDb.Id);
                }
                catch (Exception)
                {
                }

                _historicoStatusUsuarioRepository.Create(historicoUsuario);

                // Criação do UsuarioAdministrador
                UsuarioAdministrador usuarioAdm = null;
                try
                {
                    usuarioAdm = new UsuarioAdministrador("123456", usuarioAdmDb.Id);
                }
                catch (Exception)
                {
                }

                // Cria o UsuarioAdministrador no banco de dados
                _usuarioAdministradorRepository.Create(usuarioAdm);

                // Salva as Alterações na DB
                await _usuarioAdministradorRepository.UnitOfWork.SaveDbChanges();
            }
        }
    }
}
