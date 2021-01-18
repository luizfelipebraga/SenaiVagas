using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models
{
    public class QSA : AbstractDomain
    {
        public string Nome { get; set; }
        public string Qualificacao { get; set; }
        public long EmpresaId { get; set; }

        public QSA(string nome, string qualificacao, long empresaId)
        {
            Nome = nome;
            Qualificacao = qualificacao;
            EmpresaId = empresaId;
        }
    }
}
