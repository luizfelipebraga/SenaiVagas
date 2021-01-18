using Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate
{
    public interface IFaixaSalarialRepository : IRepository<FaixaSalarial>
    {
        FaixaSalarial GetByDescricao(string descricao);
    }
}
