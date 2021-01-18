using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.AreaAggregate.Models
{
    public class Area : AbstractDomain
    {
        public string Descricao { get; set; }

        public Area(string descricao)
        {
            Descricao = descricao;
        }
    }
}
