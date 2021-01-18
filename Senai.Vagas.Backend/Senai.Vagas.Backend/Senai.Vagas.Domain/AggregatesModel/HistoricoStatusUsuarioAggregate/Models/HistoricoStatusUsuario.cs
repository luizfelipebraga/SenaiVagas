using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate.Models
{
    public class HistoricoStatusUsuario : AbstractDomain
    {
        public bool Atual { get; set; }
        public long StatusUsuarioId { get; set; }
        public long UsuarioId { get; set; }


        public HistoricoStatusUsuario(long statusUsuarioId, long usuarioId)
        {
            StatusUsuarioId = statusUsuarioId;
            UsuarioId = usuarioId;
            Atual = true;
        }

        public void AlterarParaAntigo()
        {
            Atual = false;
        }
    }
}

