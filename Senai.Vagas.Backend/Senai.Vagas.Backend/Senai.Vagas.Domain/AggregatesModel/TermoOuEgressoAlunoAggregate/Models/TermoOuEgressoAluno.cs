using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate.Models
{
    public class TermoOuEgressoAluno : AbstractDomain
    {
        public string Descricao { get; set; }

        public TermoOuEgressoAluno(string descricao)
        {
            Descricao = descricao;
        }
    }
}
