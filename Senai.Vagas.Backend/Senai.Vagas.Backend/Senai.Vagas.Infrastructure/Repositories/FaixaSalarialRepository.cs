using Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate;
using Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class FaixaSalarialRepository : IFaixaSalarialRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public FaixaSalarialRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }
        public FaixaSalarial Create(FaixaSalarial objeto)
        {
            return _ctx.FaixaSalariais.Add(objeto).Entity;
        }

        public FaixaSalarial GetById(long id)
        {
            return _ctx.FaixaSalariais.FirstOrDefault(x => x.Id == id);
        }

        public FaixaSalarial GetByDescricao(string descricao)
        {
            return _ctx.FaixaSalariais.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
