using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Senai.Vagas.Backend.Application.DTOs.Inputs;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Backend.Helpers.Services;
using Senai.Vagas.Backend.Helpers.Utils;
using Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate;
using Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate;
using Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate.Models;

namespace Senai.Vagas.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        public IUsuarioRepository _usuarioRepository { get; set; }
        public ITipoUsuarioRepository _tipoUsuarioRepository { get; set; }
        public IEmpresaRepository _empresaRepository { get; set; }
        public IUsuarioEmpresaRepository _usuarioEmpresaRepository { get; set; }
        public IEnderecoRepository _enderecoRepository { get; set; }
        public ITipoEmpresaRepository _tipoEmpresaRepository { get; set; }
        public ITipoAtividadeCnaeRepository _tipoAtividadeCnaeRepository { get; set; }
        public IAlterarCredenciaisRepository _alterarCredenciaisRepository { get; set; }
        public IAlunoRepository _alunoRepository { get; set; }
        public IUsuarioCandidatoAlunoRepository _usuarioCandidatoAlunoRepository { get; set; }
        public IValidacaoUsuarioCandidatoRepository _validacaoUsuarioCandidatoRepository { get; set; }
        public IUsuarioAdministradorRepository _usuarioAdministradorRepository { get; set; }
        public IHistoricoStatusUsuarioRepository _historicoStatusUsuarioRepository { get; set; }
        public IStatusUsuarioRepository _statusUsuarioRepository { get; set; }
        public ApiRequestService apiRequestService { get; set; }
        public SendEmailService sendEmailService { get; set; }

        public UsuariosController(IUsuarioRepository usuarioRepository, ITipoUsuarioRepository tipoUsuarioRepository, IEmpresaRepository empresaRepository, IUsuarioEmpresaRepository usuarioEmpresaRepository, IEnderecoRepository enderecoRepository, ITipoEmpresaRepository tipoEmpresaRepository, ITipoAtividadeCnaeRepository tipoAtividadeCnaeRepository, IAlterarCredenciaisRepository alterarCredenciaisRepository, IAlunoRepository alunoRepository, IUsuarioCandidatoAlunoRepository usuarioCandidatoAlunoRepository, IValidacaoUsuarioCandidatoRepository validacaoUsuarioCandidatoRepository, IUsuarioAdministradorRepository usuarioAdministradorRepository, IHistoricoStatusUsuarioRepository historicoStatusUsuarioRepository, IStatusUsuarioRepository statusUsuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
            _tipoUsuarioRepository = tipoUsuarioRepository;
            _empresaRepository = empresaRepository;
            _usuarioEmpresaRepository = usuarioEmpresaRepository;
            _enderecoRepository = enderecoRepository;
            _tipoEmpresaRepository = tipoEmpresaRepository;
            _tipoAtividadeCnaeRepository = tipoAtividadeCnaeRepository;
            _alterarCredenciaisRepository = alterarCredenciaisRepository;
            _alunoRepository = alunoRepository;
            _usuarioCandidatoAlunoRepository = usuarioCandidatoAlunoRepository;
            _validacaoUsuarioCandidatoRepository = validacaoUsuarioCandidatoRepository;
            _usuarioAdministradorRepository = usuarioAdministradorRepository;
            _historicoStatusUsuarioRepository = historicoStatusUsuarioRepository;
            _statusUsuarioRepository = statusUsuarioRepository;
            apiRequestService = new ApiRequestService();
            sendEmailService = new SendEmailService();
        }

        /// <summary>
        /// Alterar credenciais
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("solicitar/alterar-credenciais")]
        public async Task<IActionResult> SolicitacaoAlterarCredenciais(SolicitacaoAlterarCredenciaisInput input)
        {
            var usuarioDb = _usuarioRepository.GetUsuarioByEmail(input.EmailAtual);

            if (usuarioDb == null)
                return StatusCode(404, $"Não foi encontrado um usuário com o email [{input.EmailAtual}].");

            var alterarCredenciaisDb = _alterarCredenciaisRepository.GetAtualByUsuarioId(usuarioDb.Id);

            if (alterarCredenciaisDb != null)
            {
                alterarCredenciaisDb.AlterarParaInativo();
                _alterarCredenciaisRepository.UpdateAlterarCredenciais(alterarCredenciaisDb);
            }

            var token = GenerationTokenUtil.TokenDefault();

            AlterarCredenciais alterarCredenciais = null;
            try
            {
                if (string.IsNullOrEmpty(input.NovoEmail))
                    alterarCredenciais = new AlterarCredenciais(token, usuarioDb.Id);
                else
                    alterarCredenciais = new AlterarCredenciais(token, input.NovoEmail, usuarioDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar o objeto AlterarCredenciais.");
            }

            _alterarCredenciaisRepository.Create(alterarCredenciais);

            string propAtualizada = string.IsNullOrEmpty(input.NovoEmail) == true ? "Senha" : "Email";

            string emailEnviado = string.IsNullOrEmpty(input.NovoEmail) == true ? usuarioDb.Email : input.NovoEmail;

            // Caso novo email não estiver vazio ou nulo, significa que o usuário deseja alterar seu email, caso contrário, deseja alterar sua senha
            if (!string.IsNullOrEmpty(input.NovoEmail))
            {
                // Envia email com link para alterar o email do usuário
                if (!sendEmailService.AlterarCredenciaisEmail(input.NovoEmail, usuarioDb.Nome, propAtualizada, token))
                    return StatusCode(500, "Houve um erro interno ao enviar o email para o usuário.");
            }
            else
            {
                // Envia email com link para alterar a senha do usuário
                if (!sendEmailService.AlterarCredenciaisSenha(usuarioDb.Email, usuarioDb.Nome, propAtualizada, token))
                    return StatusCode(500, "Houve um erro interno ao enviar o email para o usuário.");
            }

            await _alterarCredenciaisRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, $"Foi enviado ao email {emailEnviado} um link para alterar suas credenciais ({propAtualizada.ToLower()}). O link é válido por 30 minutos.");
        }

        /// <summary>
        /// Token para alterar credenciais
        /// </summary>
        /// <param name="token"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("alterar-credenciais/token/{token}")]
        public async Task<IActionResult> ConfirmarAlterarCredenciais(string token, ConfirmarAlterarCredenciaisInput input)
        {
            var alterarCredenciaisDb = _alterarCredenciaisRepository.GetAtualByToken(token);

            // Caso não existir
            if (alterarCredenciaisDb == null)
                return StatusCode(404, "Este Token expirou ou não existe no banco de dados.");

            // Caso não seja mais válido
            if (DateTime.Now.ToUniversalTime() > alterarCredenciaisDb.DataValida.ToUniversalTime())
            {
                // Altera solicitação "AlterarCredenciais" para inativo
                alterarCredenciaisDb.AlterarParaInativo();
                _alterarCredenciaisRepository.UpdateAlterarCredenciais(alterarCredenciaisDb);

                // Salva alterações no BD
                await _alterarCredenciaisRepository.UnitOfWork.SaveDbChanges();

                return StatusCode(400, "Este Token para validação de usuário expirou.");
            }

            // Busca usuario por Id
            var usuarioDb = _usuarioRepository.GetById(alterarCredenciaisDb.UsuarioId);

            // Caso não encontre
            if (usuarioDb == null)
                return StatusCode(400, "Este usuário não existe no banco de dados.");

            try
            {
                if (string.IsNullOrEmpty(alterarCredenciaisDb.NovoEmail))
                {
                    // Gera Hash da senha
                    var senhaHash = new PasswordHasher<ConfirmarAlterarCredenciaisInput>().HashPassword(input, input.NovaSenha);

                    // Altera senha do usuário
                    usuarioDb.AlterarSenha(senhaHash);
                }
                else
                {
                    // Altera email do usuário
                    usuarioDb.AlterarEmail(alterarCredenciaisDb.NovoEmail);
                }

                // Altera no BD
                _usuarioRepository.Update(usuarioDb);

                // Altera solicitação "AlterarCredenciais" para inativo
                alterarCredenciaisDb.AlterarParaInativo();

                // Altera no BD
                _alterarCredenciaisRepository.UpdateAlterarCredenciais(alterarCredenciaisDb);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar ao alterar as credenciais do usuário.");
            }

            await _usuarioRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, "As suas credenciais foram atualizadas com sucesso!");
        }

        /// <summary>
        /// Cadastrar um usuário-empresa
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("cadastro/usuario-empresa")]
        public async Task<IActionResult> CriarUsuarioEmpresa(CriarUsuarioEmpresaInput input)
        {
            if (!ValidatorUtil.CnpjIsValid(input.Cnpj))
                return StatusCode(400, "CNPJ Inválido.");

            var usuarioDb = _usuarioRepository.GetUsuarioByEmail(input.Email);

            // Verifica se já existe um usuário com mesmo email cadastrado no sistema
            if (usuarioDb != null)
                return StatusCode(400, $"Já existe um usuário com email [{input.Email}] no sistema.");

            // Verifica se existe um aluno cadastrado com mesmo email
            var alunoDb = _alunoRepository.BuscarPorEmail(input.Email);

            if (alunoDb != null)
                return StatusCode(400, $"Já existe um aluno cadastrado com email [{input.Email}] no sistema.");

            var empresaDb = _empresaRepository.GetByCNPJ(input.Cnpj);

            if (empresaDb == null)
            {
                #region Verifica se já existe uma empresa com o mesmo CNPJ cadastrada no sistema
                var responseReceita = await apiRequestService.ReceitaWS(input.Cnpj);

                if (responseReceita == null)
                    return StatusCode(400, "Ocorreu um erro na busca do CNPJ da Empresa, tente novamente mais tarde.");

                if (responseReceita.Status.Equals("ERROR") || responseReceita.Status.Equals("CNPJ inválido") || !responseReceita.Status.Equals("OK"))
                    return StatusCode(400, $"{responseReceita.Message}");

                #region Cadastra/Busca endereços e vincula-os

                TipoEmpresa tipoEmpresa = null;

                if (responseReceita.TipoEmpresa.ToUpper() == TipoEmpresaDefaultValuesAccess.GetValue(TipoEmpresaDefaultValues.FILIAL))
                    tipoEmpresa = _tipoEmpresaRepository.GetByDescricao(TipoEmpresaDefaultValuesAccess.GetValue(TipoEmpresaDefaultValues.FILIAL));

                if (responseReceita.TipoEmpresa.ToUpper() == TipoEmpresaDefaultValuesAccess.GetValue(TipoEmpresaDefaultValues.MATRIZ))
                    tipoEmpresa = _tipoEmpresaRepository.GetByDescricao(TipoEmpresaDefaultValuesAccess.GetValue(TipoEmpresaDefaultValues.MATRIZ));

                if (tipoEmpresa == null)
                    return StatusCode(400, "Houve um erro ao adquirir o tipo da empresa.");

                var responseViaCep = await apiRequestService.ViaCEP(FormatStringUtil.CaracterClear(responseReceita.CEP));

                if (responseViaCep == null)
                    return StatusCode(400, "Ocorreu um erro na busca do endereço da Empresa, tente novamente mais tarde.");

                var UfSiglaDb = _enderecoRepository.GetUfSiglaBySigla(responseViaCep.UfSigla);

                if (UfSiglaDb == null)
                    return StatusCode(400, "Não existe UF's necessárias cadastradas no banco de dados, não é possível prosseguir com o cadastro da empresa.");

                var municipioDb = _enderecoRepository.GetMunicipioByDescricao(responseViaCep.Localidade);

                // Cria um novo município caso não encontre já criado no banco de dados.
                if (municipioDb == null)
                {
                    try
                    {
                        municipioDb = new Municipio(responseViaCep.Localidade, UfSiglaDb.Id);

                        municipioDb = _enderecoRepository.CreateMunicipio(municipioDb);
                    }
                    catch (Exception)
                    {
                        return StatusCode(500, "Houve um erro interno ao criar um novo município no banco de dados para a Empresa.");
                    }
                }

                // Verifica quais campos que não estão vazios para criar o endereço
                string logradouroPreenchido = string.IsNullOrEmpty(responseViaCep.Logradouro) == false ? responseViaCep.Logradouro : responseReceita.Logradouro;
                string bairroPreenchido = string.IsNullOrEmpty(responseViaCep.Bairro) == false ? responseViaCep.Bairro : responseReceita.Bairro;

                // Cria novo endereço para a Empresa
                Endereco endereco = null;
                try
                {
                    endereco = new Endereco(responseViaCep.Cep, bairroPreenchido, logradouroPreenchido, responseReceita.Numero, municipioDb.Id);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Houve um erro interno criar um endereço para a Empresa.");
                }

                var enderecoDb = _enderecoRepository.Create(endereco);

                #endregion

                // Verifica se fantasia esta nula e substitui por nome, caso contrário, mantem fantasia
                string nomeEmpresa = string.IsNullOrEmpty(responseReceita.Fantasia) ? responseReceita.Nome : responseReceita.Fantasia;

                // Cria Empresa
                empresaDb = null;
                try
                {
                    empresaDb = new Empresa(input.Cnpj, nomeEmpresa, enderecoDb.Id, tipoEmpresa.Id);
                }
                catch (Exception)
                {
                    return StatusCode(500, "Houve um erro interno ao criar uma Empresa.");
                }

                // Cria Empresa no BD
                empresaDb = _empresaRepository.Create(empresaDb);

                #region Registra todos os TiposCnaes (Atividade Principal e Atividade Secundária)

                // Default Values Atividade principal e secundária
                var AtividadePrincipalDb = _tipoAtividadeCnaeRepository.GetByDescricao(TipoAtividadeCnaeDefaultValuesAccess.GetValue(TipoAtividadeCnaeDefaultValues.AtividadePrincipal));
                var AtividadeSecundariaDb = _tipoAtividadeCnaeRepository.GetByDescricao(TipoAtividadeCnaeDefaultValuesAccess.GetValue(TipoAtividadeCnaeDefaultValues.AtividadeSecundaria));

                // Atividade Principal
                TipoCnae tipoCnaeDb = null;

                if (responseReceita.AtividadePrincipal.Any())
                {
                    foreach (var ap in responseReceita.AtividadePrincipal)
                    {
                        tipoCnaeDb = _empresaRepository.GetTipoCnaeByCodigo(ap.Code);

                        if (tipoCnaeDb == null)
                        {
                            try
                            {
                                tipoCnaeDb = new TipoCnae(ap.Text, ap.Code);

                                tipoCnaeDb = _empresaRepository.CreateTipoCnae(tipoCnaeDb);
                            }
                            catch (Exception)
                            {
                                return StatusCode(500, "Houve um erro interno ao criar a Atividade Principal da Empresa, tente novamente mais tarde.");
                            }
                        }

                        AtividadeCnae atividadeCnae = null;
                        try
                        {
                            atividadeCnae = new AtividadeCnae(tipoCnaeDb.Id, AtividadePrincipalDb.Id, empresaDb.Id);

                            _empresaRepository.CreateAtividadeCnae(atividadeCnae);
                        }
                        catch (Exception)
                        {
                            return StatusCode(500, "Houve um erro interno ao vincular o CNAE à Empresa, tente novamente mais tarde.");
                        }
                    }
                }

                // Atividades Secundárias
                if (responseReceita.AtividadeSecundaria.Any())
                {
                    foreach (var ats in responseReceita.AtividadeSecundaria)
                    {
                        tipoCnaeDb = _empresaRepository.GetTipoCnaeByCodigo(ats.Code);

                        if (tipoCnaeDb == null)
                        {
                            try
                            {
                                tipoCnaeDb = new TipoCnae(ats.Text, ats.Code);

                                tipoCnaeDb = _empresaRepository.CreateTipoCnae(tipoCnaeDb);
                            }
                            catch (Exception)
                            {
                                return StatusCode(500, "Houve um erro interno ao criar alguma Atividade Secundária da Empresa, tente novamente mais tarde.");
                            }
                        }

                        AtividadeCnae atividadeCnae = null;
                        try
                        {
                            atividadeCnae = new AtividadeCnae(tipoCnaeDb.Id, AtividadeSecundariaDb.Id, empresaDb.Id);

                            _empresaRepository.CreateAtividadeCnae(atividadeCnae);
                        }
                        catch (Exception)
                        {
                            return StatusCode(500, "Houve um erro interno ao vincular o CNAE à Empresa, tente novamente mais tarde.");
                        }
                    }
                }

                #endregion

                // Registra todos da lista de QSA
                if (responseReceita.QSA.Any())
                {
                    foreach (var qsa in responseReceita.QSA)
                    {
                        QSA qsaDb = null;
                        try
                        {
                            qsaDb = new QSA(qsa.Nome, qsa.Qual, empresaDb.Id);

                            _empresaRepository.CreateQSA(qsaDb);
                        }
                        catch (Exception)
                        {
                            return StatusCode(500, "Houve um erro interno ao criar o QSA da Empresa.");
                        }
                    }
                }
                #endregion
            }
            else
            {
                #region Caso empresa já exista
                var usuarioEmpresaDb = _usuarioEmpresaRepository.GetByEmpresaId(empresaDb.Id);

                // Verifica se já existe um usuário vinculado à empresa
                if (usuarioEmpresaDb != null)
                    return StatusCode(400, $"A Empresa de CNPJ [{input.Cnpj}] já está vinculada à um usuário.");
                #endregion
            }

            var tipoEmpresaDb = _tipoUsuarioRepository.GetByDescricao(TipoUsuarioDefaultValuesAccess.GetValue(TipoUsuarioDefaultValues.Empresa));
            var statusUsuarioDb = _statusUsuarioRepository.GetByDescricao(StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaAtiva));

            // Criação do usuário
            usuarioDb = null;
            try
            {
                // Gera Hash da senha
                var senhaHash = new PasswordHasher<Usuario>().HashPassword(usuarioDb, input.Senha);

                usuarioDb = new Usuario(input.Nome, input.Email, senhaHash, tipoEmpresaDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar o Usuário.");
            }

            usuarioDb = _usuarioRepository.Create(usuarioDb);

            // Cria o 1° histórico do usuário
            HistoricoStatusUsuario historicoUsuario = null;
            try
            {
                historicoUsuario = new HistoricoStatusUsuario(statusUsuarioDb.Id, usuarioDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar o histórico do usuário.");
            }

            _historicoStatusUsuarioRepository.Create(historicoUsuario);

            // Criação do UsuarioEmpresa, vincula o usuário à empresa criada
            UsuarioEmpresa usuarioEmpresa = null;
            try
            {
                usuarioEmpresa = new UsuarioEmpresa(empresaDb.Id, usuarioDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar o UsuarioEmpresa (vincular o usuário à empresa).");
            }

            _usuarioEmpresaRepository.Create(usuarioEmpresa);

            await _usuarioRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, $"Usuário {usuarioDb.Email} com perfil de Empresa com CNPJ {empresaDb.CNPJ} criado com sucesso!");
        }

        /// <summary>
        /// Validar um candidato
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("validacao/candidato")]
        public async Task<IActionResult> CriarValidacaoUsuarioCandidato(CriarValidacaoUsuarioCandidatoInput input)
        {
            // Busca aluno por RMA ou Email
            var alunoDb = _alunoRepository.BuscarPorEmailouRMA(input.RmaouEmail);

            // Caso for nulo, não existe aluno
            if (alunoDb == null)
                return StatusCode(404, $"Não existe um aluno com estes dados.");

            // Busca usuario por email para verificar se já existe algum candidato com este email
            var usuarioDb = _usuarioRepository.GetUsuarioByEmail(alunoDb.Email);

            if (usuarioDb != null)
                return StatusCode(400, "Já existe um usuário com este e-mail.");

            // Busca referencia de aluno em algum usuarioCandidato
            var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.GetByAlunoId(alunoDb.Id);

            // Caso existir referencia, já existe usuário vinculado à este aluno
            if (usuarioCandidatoDb != null)
                return StatusCode(400, $"O aluno [{alunoDb.NomeCompleto}] já é vinculado à um usuário.");

            // Caso aluno não tiver email, não será possível criar o cadastro
            if (string.IsNullOrEmpty(alunoDb.Email))
                return StatusCode(404, $"O aluno [{alunoDb.NomeCompleto}] não tem um e-mail cadastrado. Contate a administração do SENAI para atualizar seu cadastro, para assim ser possível o prosseguimento da criação de sua conta no SENAI Vagas.");

            string token = "";

            ValidacaoUsuarioCandidato validacaoUsuario = null;
            try
            {
                // Verifica se já existe no BD uma validacao com o mesmo aluno existente
                var validacaoAntiga = _validacaoUsuarioCandidatoRepository.GetValidacaoUsuarioCandidatoAtualByAlunoId(alunoDb.Id);

                // Caso existir, altera para antiga
                if (validacaoAntiga != null)
                {
                    validacaoAntiga.AlterarParaInativo();
                    _validacaoUsuarioCandidatoRepository.UpdateValidacaoUsuarioCandidato(validacaoAntiga);
                }

                // Cria a nova validação
                token = GenerationTokenUtil.TokenDefault();
                validacaoUsuario = new ValidacaoUsuarioCandidato(token, alunoDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar a validação de usuário.");
            }

            // Cria validação de usuário no BD
            _validacaoUsuarioCandidatoRepository.Create(validacaoUsuario);

            // Envia email de verificação de usuário/aluno e valida se houver algum erro
            if (!sendEmailService.EmailVerificacaoCandidato(alunoDb.Email.ToLower(), alunoDb.NomeCompleto, token))
                return StatusCode(500, "Houve um erro interno ao enviar o email para validação de usuário.");

            // Salva alterações no BD
            await _validacaoUsuarioCandidatoRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, $"Foi enviado ao email {alunoDb.Email} um link de validação de usuário / Aluno. O link é válido por 30 minutos.");
        }

        /// <summary>
        /// Cadastrar um novo candidato
        /// </summary>
        /// <param name="token"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("cadastro/usuario-candidato/token/{token}")]
        public async Task<IActionResult> CriarUsuarioCandidato(string token, CriarUsuarioCandidatoAlunoInput input)
        {
            // Busca validacao de usuario por token
            var validacaoUsuario = _validacaoUsuarioCandidatoRepository.GetValidacaoUsuarioCandidatoByToken(token);

            // Caso não existir
            if (validacaoUsuario == null)
                return StatusCode(404, "Este Token expirou ou não existe no banco de dados.");

            // Caso não seja mais válido
            if (DateTime.Now.ToUniversalTime() > validacaoUsuario.DataValida.ToUniversalTime())
            {
                // Altera para inativo caso não seja mais válido
                validacaoUsuario.AlterarParaInativo();
                _validacaoUsuarioCandidatoRepository.UpdateValidacaoUsuarioCandidato(validacaoUsuario);

                // Salva alterações no BD
                await _validacaoUsuarioCandidatoRepository.UnitOfWork.SaveDbChanges();

                return StatusCode(400, "Este Token para validação de usuário expirou.");
            }

            // Busca aluno pelo Id
            var alunoDb = _alunoRepository.GetById(validacaoUsuario.AlunoId);

            // Caso aluno não exista mais
            if (alunoDb == null)
                return StatusCode(400, "O aluno não existe mais no banco de dados, não será permitido criar um usuário.");

            var tipoCandidatoDb = _tipoUsuarioRepository.GetByDescricao(TipoUsuarioDefaultValuesAccess.GetValue(TipoUsuarioDefaultValues.Candidato));
            var statusUsuarioDb = _statusUsuarioRepository.GetByDescricao(StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaAtiva));

            // Cria o usuário
            Usuario usuario = null;
            try
            {
                // Gera Hash da senha
                var senhaHash = new PasswordHasher<Usuario>().HashPassword(usuario, input.Senha);

                usuario = new Usuario(alunoDb.NomeCompleto, alunoDb.Email, senhaHash, tipoCandidatoDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar o usuário.");
            }

            var usuarioDb = _usuarioRepository.Create(usuario);

            // Cria o 1° histórico do usuário
            HistoricoStatusUsuario historicoUsuario = null;
            try
            {
                historicoUsuario = new HistoricoStatusUsuario(statusUsuarioDb.Id, usuarioDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar o histórico do usuário.");
            }

            _historicoStatusUsuarioRepository.Create(historicoUsuario);

            // Cria o usuarioCandidato
            UsuarioCandidatoAluno usuarioCandidato = null;
            try
            {
                usuarioCandidato = new UsuarioCandidatoAluno(alunoDb.Id, usuarioDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao vincular o perfil de candidato ao usuário.");
            }

            var usuarioCandidatoDb = _usuarioCandidatoAlunoRepository.Create(usuarioCandidato);

            // Cria o perfil do usuarioCandidato (linkExterno e SobreOCandidato vazios)
            PerfilUsuarioCandidatoAluno perfilCandidato = null;
            try
            {
                perfilCandidato = new PerfilUsuarioCandidatoAluno("", "", usuarioCandidatoDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar o perfil padrão do candidato.");
            }

            _usuarioCandidatoAlunoRepository.CreatePerfilUsuarioCandidatoAluno(perfilCandidato);

            // Altera para validação Inativa (para não registrar dois usuários candidatos iguais)
            validacaoUsuario.AlterarParaInativo();
            _validacaoUsuarioCandidatoRepository.UpdateValidacaoUsuarioCandidato(validacaoUsuario);

            await _usuarioCandidatoAlunoRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, $"Usuário [{alunoDb.Email}] com perfil de {tipoCandidatoDb.Descricao} criado com sucesso!");
        }

        /// <summary>
        /// Cadastrar um novo administrador (admin)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("cadastro/usuario-administrador")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> CriarUsuarioAdministrador(CriarUsuarioAdministradorInput input)
        {
            // Verifica se existe um usuário com mesmo email cadastrado no sistema
            var usuarioDb = _usuarioRepository.GetUsuarioByEmail(input.Email);

            if (usuarioDb != null)
                return StatusCode(400, "Já existe um usuário cadastrado com este email.");

            // Verifica se existe um aluno cadastrado com mesmo email
            var alunoDb = _alunoRepository.BuscarPorEmail(input.Email);

            if (alunoDb != null)
                return StatusCode(400, $"Já existe um aluno cadastrado com email [{input.Email}] no sistema.");

            var tipoAdmDb = _tipoUsuarioRepository.GetByDescricao(TipoUsuarioDefaultValuesAccess.GetValue(TipoUsuarioDefaultValues.Administrador));
            var statusUsuarioDb = _statusUsuarioRepository.GetByDescricao(StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaAtiva));

            // Cria o usuário
            Usuario usuario = null;
            try
            {
                // Gera Hash da senha
                var senhaHash = new PasswordHasher<Usuario>().HashPassword(usuario, input.Senha);

                usuario = new Usuario(input.Nome, input.Email, senhaHash, tipoAdmDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar o usuário.");
            }

            usuarioDb = _usuarioRepository.Create(usuario);

            // Cria o 1° histórico do usuário
            HistoricoStatusUsuario historicoUsuario = null;
            try
            {
                historicoUsuario = new HistoricoStatusUsuario(statusUsuarioDb.Id, usuarioDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao criar o histórico do usuário.");
            }

            _historicoStatusUsuarioRepository.Create(historicoUsuario);

            // Cria o usuário administrador
            UsuarioAdministrador usuarioAdm = null;
            try
            {
                usuarioAdm = new UsuarioAdministrador(input.Nif, usuarioDb.Id);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao vincular o usuário à um perfil de Administrador.");
            }

            _usuarioAdministradorRepository.Create(usuarioAdm);

            await _usuarioAdministradorRepository.UnitOfWork.SaveDbChanges();

            return StatusCode(200, $"Usuário [{usuarioDb.Email}] com perfil de {tipoAdmDb.Descricao} criado com sucesso!");
        }
    }
}
