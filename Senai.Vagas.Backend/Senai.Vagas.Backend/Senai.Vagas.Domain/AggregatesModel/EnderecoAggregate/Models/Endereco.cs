using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models
{
   public class Endereco : AbstractDomain
    {
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public long MunicipioId { get; set; }

        public Endereco(string cEP, string bairro, string logradouro, string numero, long municipioId)
        {
            CEP = cEP;
            Bairro = bairro;
            Logradouro = logradouro;
            Numero = numero;
            MunicipioId = municipioId;
        }

        public Endereco(string bairro, string logradouro, string numero, long municipioId)
        {
            Bairro = bairro;
            Logradouro = logradouro;
            Numero = numero;
            MunicipioId = municipioId;
        }
    }
}
