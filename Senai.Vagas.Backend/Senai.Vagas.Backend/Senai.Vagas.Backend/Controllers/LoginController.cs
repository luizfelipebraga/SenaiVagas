using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Senai.Vagas.Backend.Application.DTOs.Inputs;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }
        private IStatusUsuarioRepository _statusUsuarioRepository { get; set; }
        private IHistoricoStatusUsuarioRepository _historicoStatusUsuarioRepository { get; set; }

        public LoginController(IUsuarioRepository usuarioRepository, IStatusUsuarioRepository statusUsuarioRepository, IHistoricoStatusUsuarioRepository historicoStatusUsuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _statusUsuarioRepository = statusUsuarioRepository;
            _historicoStatusUsuarioRepository = historicoStatusUsuarioRepository;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(LoginInput login)
        {
            try
            {
                Usuario usuarioBuscado = _usuarioRepository.GetUsuarioByEmail(login.Email);
                PasswordVerificationResult validacaoSenha = PasswordVerificationResult.Failed;

                if (usuarioBuscado != null)
                {
                  validacaoSenha = new PasswordHasher<Usuario>().VerifyHashedPassword(usuarioBuscado, usuarioBuscado.Senha, login.Senha);
                }

                if (usuarioBuscado == null || validacaoSenha == PasswordVerificationResult.Failed)
                {
                    return NotFound("Email e/ou senha inválidos.");
                }

                // Busca o historicoStatusUsuario pelo ID do usuário
                HistoricoStatusUsuario historicoStatus = _historicoStatusUsuarioRepository.GetHistoricoAtualByUsuarioId(usuarioBuscado.Id);

                if (historicoStatus == null)
                    return StatusCode(404, "Não foi encontrado um histórico de status desse usuário.");

                // Busca o status "Conta Ativa"
                var statusUsuarioAtivoDb = _statusUsuarioRepository.GetByDescricao(StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaAtiva));

                // Busca o status do usuário buscado
                var statusUsuarioBuscado = _statusUsuarioRepository.GetById(historicoStatus.StatusUsuarioId);

                // O historicoStatus do usuário buscado é igual à conta ATIVA? Caso NÃO, retorna erro que o usuário não é mais autorizado a logar no site.
                if (historicoStatus.StatusUsuarioId != statusUsuarioAtivoDb.Id)
                    return Unauthorized($"O usuário [{usuarioBuscado.Email}] está com a {statusUsuarioBuscado.Descricao}.");

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),

                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),

                    new Claim(ClaimTypes.Role,usuarioBuscado.TipoUsuarioId.ToString()),

                    new Claim("role", usuarioBuscado.TipoUsuarioId.ToString()),

                    new Claim(JwtRegisteredClaimNames.UniqueName, usuarioBuscado.Nome)

                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("senaivagas-chave-autenticacao"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "Senai.Vagas.Backend",
                    audience: "Senai.Vagas.Backend",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );


                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception error)
            {
                // Retorna a resposta da requisição 400 - Bad Request e o erro ocorrido com uma mensagem
                return StatusCode(500, $"Houve um erro interno ao gerar o Token de usuário | [{error.Message}]");
            }
        }
    }
}
        

