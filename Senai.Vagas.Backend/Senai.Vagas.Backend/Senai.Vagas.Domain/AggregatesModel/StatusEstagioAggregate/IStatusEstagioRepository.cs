using Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate
{
    public interface IStatusEstagioRepository : IRepository<StatusEstagio>
    {
        StatusEstagio GetByDescricao(string descricao);
    }
}
