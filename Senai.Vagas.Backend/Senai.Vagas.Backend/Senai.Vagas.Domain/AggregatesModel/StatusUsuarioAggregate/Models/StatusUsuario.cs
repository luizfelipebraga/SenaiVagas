using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate.Models
{
   public class StatusUsuario : AbstractDomain
    {
        public string Descricao { get; set; }

        public StatusUsuario(string descricao)
        {
            Descricao = descricao;
        }
    }
}
