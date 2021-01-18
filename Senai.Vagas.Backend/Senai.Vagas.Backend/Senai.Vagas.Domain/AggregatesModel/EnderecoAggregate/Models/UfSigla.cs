using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models
{
   public class UfSigla : AbstractDomain
    {
        public string UFEstado { get; set; }
        public string UFSigla { get; set; }

        public UfSigla(string uFEstado, string uFSigla)
        {
            UFEstado = uFEstado;
            UFSigla = uFSigla;
        }
    }
}
