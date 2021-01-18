using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate.Models
{
   public class UsuarioAdministrador : AbstractDomain
    {
        public string NIF { get; set; }
        public long UsuarioId { get; set; }

        public UsuarioAdministrador(string nIF, long usuarioId)
        {
            NIF = nIF;
            UsuarioId = usuarioId;
        }
    }
}
