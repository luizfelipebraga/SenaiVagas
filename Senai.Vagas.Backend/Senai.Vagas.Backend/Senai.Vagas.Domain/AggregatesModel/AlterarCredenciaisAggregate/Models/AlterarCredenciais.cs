using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate.Models
{
    public class AlterarCredenciais : AbstractDomain
    {
        public string Token { get; set; }
        public DateTime DataValida { get; set; }
        public bool Ativo { get; set; }
        public string NovoEmail { get; set; }
        public long UsuarioId { get; set; }


        public AlterarCredenciais(string token, string novoEmail, long usuarioId)
        {

            Token = token;
            Ativo = true;
            DataValida = DateTime.Now.AddMinutes(30);
            NovoEmail = novoEmail;
            UsuarioId = usuarioId;
        }

        public AlterarCredenciais(string token, long usuarioId)
        {

            Token = token;
            Ativo = true;
            DataValida = DateTime.Now.AddMinutes(30);
            UsuarioId = usuarioId;
        }

        public void AlterarParaInativo()
        {
            Ativo = false;
        }
    }
}
