using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class StatusVagaRepository : IStatusVagaRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public StatusVagaRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public StatusVaga Create(StatusVaga objeto)
        {
            return _ctx.StatusVagas.Add(objeto).Entity;
        }

        public StatusVaga GetById(long id)
        {
            return _ctx.StatusVagas.FirstOrDefault(x => x.Id == id);
        }

        public StatusVaga GetByDescricao(string descricao)
        {
            return _ctx.StatusVagas.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
