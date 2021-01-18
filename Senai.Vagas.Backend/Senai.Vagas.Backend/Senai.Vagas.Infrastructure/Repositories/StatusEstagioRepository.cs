using Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class StatusEstagioRepository : IStatusEstagioRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public StatusEstagioRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public StatusEstagio Create(StatusEstagio objeto)
        {
            return _ctx.StatusEstagios.Add(objeto).Entity;
        }

        public StatusEstagio GetById(long id)
        {
            return _ctx.StatusEstagios.FirstOrDefault(x => x.Id == id);
        }

        public StatusEstagio GetByDescricao(string descricao)
        {
            return _ctx.StatusEstagios.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
