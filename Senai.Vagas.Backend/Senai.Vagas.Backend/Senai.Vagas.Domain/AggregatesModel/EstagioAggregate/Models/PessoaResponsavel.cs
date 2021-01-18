using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EstagioAggregate.Models
{
    public class PessoaResponsavel : AbstractDomain
    {
        public string Nome { get; set; }
        public PessoaResponsavel(string nome)
        {
            Nome = nome;
        }
    }
}
