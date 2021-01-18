using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate.Models
{
   public class StatusVaga : AbstractDomain
    {
        public string Descricao { get; set; }

        public StatusVaga(string descricao)
        {
            Descricao = descricao;
        }
    }
}
