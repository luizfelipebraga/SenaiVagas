using Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate
{
    public interface ITipoAtividadeCnaeRepository : IRepository <TipoAtividadeCnae>
    {
        TipoAtividadeCnae GetByDescricao(string descricao);
    }
}
