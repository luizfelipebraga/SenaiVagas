using Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate
{
    public interface ITermoOuEgressoAlunoRepository : IRepository<TermoOuEgressoAluno>
    {
        TermoOuEgressoAluno GetByDescricao(string descricao);
    }
}
