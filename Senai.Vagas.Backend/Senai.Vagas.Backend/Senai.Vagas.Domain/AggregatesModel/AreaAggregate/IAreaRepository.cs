using Senai.Vagas.Domain.AggregatesModel.AreaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.AreaAggregate
{
    public interface IAreaRepository : IRepository<Area>
    {
        Area GetByDescricao(string descricao);
    }
}
