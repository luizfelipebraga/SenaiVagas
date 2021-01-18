using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate.Models
{
    public class FaixaSalarial : AbstractDomain
    {
        public string Descricao { get; set; }

        public FaixaSalarial(string descricao)
        {
            Descricao = descricao;
        }
    }
}
