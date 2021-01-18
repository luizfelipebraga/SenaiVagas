using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate.Models
{
    public class TipoAtividadeCnae : AbstractDomain
    {
        public string Descricao { get; set; }

        public TipoAtividadeCnae(string descricao)
        {
            Descricao = descricao;
        }
    }
}
