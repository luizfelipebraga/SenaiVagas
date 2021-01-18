using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.ResponseViewModels
{
    public class UsuarioAdministradorViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Nif { get; set; }
        public string Email { get; set; }

        public UsuarioAdministradorViewModel(long id, string nome, string nif, string email)
        {
            Id = id;
            Nome = nome;
            Nif = nif;
            Email = email;
        }
    }
}