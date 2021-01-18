using Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class TipoExperienciaRepository : ITipoExperienciaRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public TipoExperienciaRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }
        public TipoExperiencia Create(TipoExperiencia objeto)
        {
            return _ctx.TipoExperiencias.Add(objeto).Entity;
        }

        public TipoExperiencia GetById(long id)
        {
            return _ctx.TipoExperiencias.FirstOrDefault(x => x.Id == id);
        }

        public TipoExperiencia GetByDescricao(string descricao)
        {
            return _ctx.TipoExperiencias.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
