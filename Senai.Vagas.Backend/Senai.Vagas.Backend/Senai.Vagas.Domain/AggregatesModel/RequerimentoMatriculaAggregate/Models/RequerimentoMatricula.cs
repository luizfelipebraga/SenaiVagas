using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate.Models
{
    public class RequerimentoMatricula : AbstractDomain
    {
        public string Descricao { get; set; }

        public RequerimentoMatricula(string descricao)
        {
            Descricao = descricao;
        }
    }
}
