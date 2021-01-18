using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models
{
    public class TipoCnae : AbstractDomain
    {
        public string Descricao { get; set; }
        public string Codigo { get; set; }

        public TipoCnae(string descricao, string codigo)
        {
            Descricao = descricao;
            Codigo = codigo;
        }
    }
}
