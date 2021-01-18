using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate
{
    public interface IStatusVagaRepository : IRepository<StatusVaga>
    {
        StatusVaga GetByDescricao(string descricao);
    }
}
