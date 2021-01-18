using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusEstagioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.HistoricoStatusEstagioAggregate
{
    public interface IHistoricoStatusEstagioRepository : IRepository<HistoricoStatusEstagio>
    {
        HistoricoStatusEstagio GetHistoricoAtualByEstagioId(long estagioId);
    }
}
