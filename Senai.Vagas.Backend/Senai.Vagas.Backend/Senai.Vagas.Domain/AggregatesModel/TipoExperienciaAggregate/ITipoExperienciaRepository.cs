using Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate
{
    public interface ITipoExperienciaRepository : IRepository<TipoExperiencia>
    {
        TipoExperiencia GetByDescricao(string descricao);
    }
}
