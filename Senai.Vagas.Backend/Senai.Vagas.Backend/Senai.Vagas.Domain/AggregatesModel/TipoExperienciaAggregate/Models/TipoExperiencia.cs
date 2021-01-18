using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate.Models
{
    public class TipoExperiencia : AbstractDomain
    {
        public string Descricao { get; set; }

        public TipoExperiencia(string descricao)
        {
            Descricao = descricao;
        }
    }
}
