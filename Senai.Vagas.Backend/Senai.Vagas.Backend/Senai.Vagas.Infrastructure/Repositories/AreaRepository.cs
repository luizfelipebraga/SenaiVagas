using Senai.Vagas.Domain.AggregatesModel.AreaAggregate;
using Senai.Vagas.Domain.AggregatesModel.AreaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public AreaRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public Area Create(Area objeto)
        {
            return _ctx.Areas.Add(objeto).Entity;
        }

        public Area GetByDescricao(string descricao)
        {
            return _ctx.Areas.FirstOrDefault(x => x.Descricao == descricao);
        }

        public Area GetById(long id)
        {
            return _ctx.Areas.FirstOrDefault(x => x.Id == id);
        }
    }
}
