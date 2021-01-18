using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models
{
   public class Municipio : AbstractDomain
    {
        public string Descricao { get; set; }
        public long UfSiglaId { get; set; }

        public Municipio(string descricao, long ufSiglaId)
        {
            Descricao = descricao;
            UfSiglaId = ufSiglaId;
        }
    }
}
