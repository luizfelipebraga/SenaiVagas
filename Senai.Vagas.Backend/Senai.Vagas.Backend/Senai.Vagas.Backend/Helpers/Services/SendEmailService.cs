using Senai.Vagas.Backend.Helpers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Services
{
    public class SendEmailService
    {
        private string Email { get; set; }
        private string Senha { get; set; }
        private string Url { get; set; }

        public SendEmailService()
        {
            var credenciais = ReadingJsonUtil.GetCredenciaisEmail();
            var url = ReadingJsonUtil.GetUrlFrontend();

            Email = credenciais.EmailRemetente;
            Senha = credenciais.Senha;
            Url = url.Url;
        }

        public bool EmailVerificacaoCandidato(string destinatario, string nomeAluno, string token)
        {
            try
            {
                // Remetente / Destinatário
                MailMessage _mailMessage = new MailMessage(Email, destinatario);

                //Contrói o MailMessage
                _mailMessage.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                _mailMessage.BodyEncoding = Encoding.GetEncoding("UTF-8");
                _mailMessage.Subject = "SENAI Vagas - Validação de Aluno/Usuário.";
                _mailMessage.IsBodyHtml = true;

                //Configuração Head
                _mailMessage.Body += "<!DOCTYPE html><html lang=\"pt-br\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\"><title> Validação de Candidato Aluno </title></head>";
                //Configuração Body
                _mailMessage.Body += "<body style=\"margin:0px; padding:0px;box-sizing: border-box; font-size: 100%;\">";
                //Logo e Mensagem
                _mailMessage.Body += $"<h1 style=\"color: #df0002; font-family: Allan; font-size: 2.2em; text-align: center;\"> SENAI Vagas </h1><p style=\"color:black\"> Olá {nomeAluno}! Este é um email automático para validação de usuário/aluno na plataforma SENAI Vagas. Você solicitou uma criação de conta como candidato, e para validar sua conta clique no link abaixo. <br> Se você nao solicitou essa ação, por favor ignore esta mensagem.</p>";
                //Link / Fechamento Body e Html
                _mailMessage.Body += $"<div><a target=\"blank\" href=\" {Url}novas-credenciais/path/validacao/token/{token}\" style=\"display: flex; justify-content: center;\">Link de Verificação</a></div></body></html>";

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential(this.Email, this.Senha);

                _smtpClient.EnableSsl = true;

                _smtpClient.Send(_mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AlterarCredenciaisSenha(string destinatario, string nomeUsuario, string tipoCredencial, string token)
        {
            try
            {
                // Remetente / Destinatário
                MailMessage _mailMessage = new MailMessage(Email, destinatario);

                //Contrói o MailMessage
                _mailMessage.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                _mailMessage.BodyEncoding = Encoding.GetEncoding("UTF-8");
                _mailMessage.Subject = "SENAI Vagas - Solicitação de alteração de credenciais.";
                _mailMessage.IsBodyHtml = true;

                //Configuração Head
                _mailMessage.Body += "<!DOCTYPE html><html lang=\"pt-br\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\"><title> Solicitação de alteração de credenciais </title></head>";
                //Configuração Body
                _mailMessage.Body += "<body style=\"margin:0px; padding:0px;box-sizing: border-box; font-size: 100%;\">";
                //Logo e Mensagem
                _mailMessage.Body += $"<h1 style=\"color: #df0002; font-family: Allan; font-size: 2.2em; text-align: center;\"> SENAI Vagas </h1><p style=\"color:black\"> Olá {nomeUsuario}! Você solicitou uma ação para atualização de suas credenciais ({tipoCredencial.ToUpper()}). Clique no link abaixo para alterar suas credenciais. <br> Se você nao solicitou essa ação, por favor ignore esta mensagem.</p>";
                //Link / Fechamento Body e Html
                _mailMessage.Body += $"<div><a target=\"blank\" href=\" {Url}novas-credenciais/path/{tipoCredencial.ToLower()}/token/{token}\" style=\"display: flex; justify-content: center;\">Link para Alterar suas Credenciais</a></div></body></html>";

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential(this.Email, this.Senha);

                _smtpClient.EnableSsl = true;

                _smtpClient.Send(_mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool AlterarCredenciaisEmail(string destinatario, string nomeUsuario, string tipoCredencial, string token)
        {
            try
            {
                // Remetente / Destinatário
                MailMessage _mailMessage = new MailMessage(Email, destinatario);

                //Contrói o MailMessage
                _mailMessage.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                _mailMessage.BodyEncoding = Encoding.GetEncoding("UTF-8");
                _mailMessage.Subject = "SENAI Vagas - Solicitação de alteração de credenciais.";
                _mailMessage.IsBodyHtml = true;

                //Configuração Head
                _mailMessage.Body += "<!DOCTYPE html><html lang=\"pt-br\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><meta http-equiv=\"X-UA-Compatible\" content=\"ie=edge\"><title> Solicitação de alteração de credenciais </title></head>";
                //Configuração Body
                _mailMessage.Body += "<body style=\"margin:0px; padding:0px;box-sizing: border-box; font-size: 100%;\">";
                //Logo e Mensagem
                _mailMessage.Body += $"<h1 style=\"color: #df0002; font-family: Allan; font-size: 2.2em; text-align: center;\"> SENAI Vagas </h1><p style=\"color:black\"> Olá {nomeUsuario}! Você solicitou uma ação para que este email seja utilizado como padrão de sua conta na plataforma SENAI Vagas. Clique no link abaixo para liberar a atualização de seu email. <br> Se você nao solicitou essa ação, por favor ignore esta mensagem.</p>";
                //Link / Fechamento Body e Html
                _mailMessage.Body += $"<div><a target=\"blank\" href=\" {Url}novas-credenciais/path/{tipoCredencial.ToLower()}/token/{token}\" style=\"display: flex; justify-content: center;\">Link para Alterar suas Credenciais</a></div></body></html>";

                //CONFIGURAÇÃO COM PORTA
                SmtpClient _smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                _smtpClient.UseDefaultCredentials = false;
                _smtpClient.Credentials = new NetworkCredential(this.Email, this.Senha);

                _smtpClient.EnableSsl = true;

                _smtpClient.Send(_mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
