using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate.Models
{
    public class TipoEmpresa : AbstractDomain
    {
        public string Descricao { get; set; }

        public TipoEmpresa(string descricao)
        {
            Descricao = descricao;
        }
    }
}
