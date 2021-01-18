using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models
{
    public class Usuario : AbstractDomain
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public long TipoUsuarioId { get; set; }

        public Usuario(string nome, string email, string senha, long tipoUsuarioId)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
            TipoUsuarioId = tipoUsuarioId;
        }

        public void AlterarSenha(string senha)
        {
            if(!string.IsNullOrEmpty(senha))
                Senha = senha;
        }
        public void AlterarEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
                Email = email;
        }
    }
}
