using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models
{
    public class Empresa : AbstractDomain
    {
        public string CNPJ { get; set; }
        public string Nome { get; set; }
        public long EnderecoId { get; set; }
        public long TipoEmpresaId { get; set; }

        public Empresa(string cNPJ, string nome, long enderecoId, long tipoEmpresaId)
        {
            CNPJ = cNPJ;
            Nome = nome;
            EnderecoId = enderecoId;
            TipoEmpresaId = tipoEmpresaId;
        }
    }
}
