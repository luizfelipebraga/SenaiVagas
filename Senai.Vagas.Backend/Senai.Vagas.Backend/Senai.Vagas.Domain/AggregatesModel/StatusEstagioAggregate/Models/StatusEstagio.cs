using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate.Models
{
   public class StatusEstagio : AbstractDomain
    {
        public string Descricao { get; set; }

        public StatusEstagio(string descricao)
        {
            Descricao = descricao;
        }
    }
}
