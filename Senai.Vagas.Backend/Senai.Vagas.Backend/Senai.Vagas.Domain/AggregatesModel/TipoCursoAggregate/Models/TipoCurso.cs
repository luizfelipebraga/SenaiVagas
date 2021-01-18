using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate.Models
{
   public class TipoCurso : AbstractDomain
    {
        public string Descricao { get; set; }

        public TipoCurso(string descricao)
        {
            Descricao = descricao;
        }
    }
}
