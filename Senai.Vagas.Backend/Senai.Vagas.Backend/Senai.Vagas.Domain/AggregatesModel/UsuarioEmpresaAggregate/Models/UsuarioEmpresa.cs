using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models
{
   public class UsuarioEmpresa : AbstractDomain
    {
        public long EmpresaId { get; set; }
        public long UsuarioId { get; set; }

        public UsuarioEmpresa(long empresaId, long usuarioId)
        {
            EmpresaId = empresaId;
            UsuarioId = usuarioId;
        }
    }
}
