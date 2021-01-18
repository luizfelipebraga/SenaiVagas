using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate;

namespace Senai.Vagas.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        public IEmpresaQueries _empresasQueries { get; set; }
        public IUsuarioRepository _usuarioRepository { get; set; }
        public IStatusUsuarioRepository _statusUsuarioRepository { get; set; }
        public IHistoricoStatusUsuarioRepository _historicoStatusUsuarioRepository { get; set; }
        public IUsuarioEmpresaRepository _usuarioEmpresaRepository { get; set; }

        public EmpresasController(IEmpresaQueries empresasQueries, IUsuarioRepository usuarioRepository, IStatusUsuarioRepository statusUsuarioRepository, IHistoricoStatusUsuarioRepository historicoStatusUsuarioRepository, IUsuarioEmpresaRepository usuarioEmpresaRepository)
        {
            _empresasQueries = empresasQueries;
            _usuarioRepository = usuarioRepository;
            _statusUsuarioRepository = statusUsuarioRepository;
            _historicoStatusUsuarioRepository = historicoStatusUsuarioRepository;
            _usuarioEmpresaRepository = usuarioEmpresaRepository;
        }

        /// <summary>
        /// Buscar perfil da empresa (empresa)
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("buscar/usuario/{usuarioId}")]
        public IActionResult PerfilEmpresaGetEmpresaByUsuarioId(long usuarioId)
        {
            // Busca usuário por Id
            var usuarioDb = _usuarioRepository.GetById(usuarioId);

            // Caso não existir
            if (usuarioDb == null)
                return StatusCode(404, $"Este Id de usuário [{usuarioId}] não existe.");

            // Busca o vinculo entre usuario X empresa
            var usuarioEmpresaDb = _usuarioEmpresaRepository.GetByUsuarioId(usuarioDb.Id);

            // Caso não achar, o usuário não é uma empresa
            if (usuarioEmpresaDb == null)
                return StatusCode(400, "Este usuário não é uma Empresa.");

            EmpresaViewModel empresa = null;
            try
            {
                empresa = _empresasQueries.GetEmpresaById(usuarioEmpresaDb.EmpresaId);

                if (empresa == null)
                    return StatusCode(404, $"Não foi encontrado uma empresa com id [{usuarioEmpresaDb.EmpresaId}] no banco de dados.");

                empresa.AcrescentarEmailUsuario(usuarioDb.Email);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao buscar a empresa específica.");
            }

            return StatusCode(200, empresa);
        }

        /// <summary>
        /// Buscar perfil da empresa especifica (admin)
        /// </summary>
        /// <param name="empresaId"></param>
        /// <returns></returns>
        [HttpGet("buscar/empresa/{empresaId}")]
        public IActionResult AdminGetEmpresaByEmpresaId(long empresaId)
        {
            // TODO: Arrumar ESTAGIOS para que os UsuariosEmpresas precisam ser cadastradas para ter estagios cadastrados
            EmpresaViewModel empresa = null;
            try
            {
                empresa = _empresasQueries.GetEmpresaById(empresaId);

                if (empresa == null)
                    return StatusCode(404, $"Não foi encontrado uma empresa com id [{empresaId}] no banco de dados.");

                // Busca o vinculo entre usuario X empresa
                var usuarioEmpresaDb = _usuarioEmpresaRepository.GetByEmpresaId(empresa.Id);

                if (usuarioEmpresaDb == null)
                    return StatusCode(404, $"Não foi encontrado um usuário com a empresa id [{empresaId}]");

                // Busca usuário por Id
                var usuarioDb = _usuarioRepository.GetById(usuarioEmpresaDb.UsuarioId);

                // Caso não existir
                if (usuarioDb == null)
                    return StatusCode(404, $"Este Id de usuário [{usuarioEmpresaDb.UsuarioId}] não existe.");

                empresa.AcrescentarEmailUsuario(usuarioDb.Email);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao buscar a empresa específica.");
            }

            return StatusCode(200, empresa);
        }

        /// <summary>
        /// Buscar todas as empresas (admin)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "3")]
        public IActionResult AdminGetAllEmpresas()
        {
            List<EmpresaViewModel> empresas = new List<EmpresaViewModel>();
            try
            {
                empresas = _empresasQueries.GetAllEmpresas();
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao buscar todas as empresas cadastradas na plataforma.");
            }

            // Itera entre todas as empresas
            foreach (var empresa in empresas.ToList())
            {
                try
                {
                    // Busca o usuarioEmpresa por ID
                    var usuarioEmpresa = _usuarioEmpresaRepository.GetByEmpresaId(empresa.Id);

                    // Busca usuário por Id
                    var usuarioDb = _usuarioRepository.GetById(usuarioEmpresa.UsuarioId);

                    // Caso não existir
                    if (usuarioDb == null)
                        return StatusCode(404, $"Este Id de usuário [{usuarioEmpresa.UsuarioId}] não existe.");

                    // Busca o histórico do usuário por ID
                    var historicoUsuario = _historicoStatusUsuarioRepository.GetHistoricoAtualByUsuarioId(usuarioDb.Id);

                    // Caso historico do usuário não exista
                    if (historicoUsuario == null)
                        return StatusCode(404, $"Não foi encontrado um histórico de usuário da empresa [{empresa.Id}]");

                    // Busca o statusUsuario "Conta Excluída"
                    var statusUsuarioExcluidoDb = _statusUsuarioRepository.GetByDescricao(StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaExcluida));

                    // Caso a conta da empresa esteja com status EXCLUÍDA, remove a empresa específica da lista
                    if (historicoUsuario.StatusUsuarioId == statusUsuarioExcluidoDb.Id)
                        empresas.Remove(empresa);
                    else
                    {
                        // Caso estiver ATIVA ou DESATIVADA, pode ser visualizada e vincula email do usuário com a empresa
                        empresa.AcrescentarEmailUsuario(usuarioDb.Email);
                    }
                }
                catch (Exception)
                {
                    return StatusCode(500, "Houve algum erro interno ao verificar o histórico de status usuários das empresas.");
                }
            }

            return StatusCode(200, empresas);
        }
    }
}
