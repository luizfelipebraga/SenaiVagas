using Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class TipoAtividadeCnaeRepository : ITipoAtividadeCnaeRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public TipoAtividadeCnaeRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public TipoAtividadeCnae Create(TipoAtividadeCnae objeto)
        {
            return _ctx.TipoAtividadeCnaes.Add(objeto).Entity;
        }

        public TipoAtividadeCnae GetById(long id)
        {
            return _ctx.TipoAtividadeCnaes.FirstOrDefault(x => x.Id == id);
        }

        public TipoAtividadeCnae GetByDescricao(string descricao)
        {
            return _ctx.TipoAtividadeCnaes.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
