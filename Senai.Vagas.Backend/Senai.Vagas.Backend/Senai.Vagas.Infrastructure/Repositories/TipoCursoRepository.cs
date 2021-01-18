using Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class TipoCursoRepository : ITipoCursoRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public TipoCursoRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public TipoCurso Create(TipoCurso objeto)
        {
            return _ctx.TipoCursos.Add(objeto).Entity;
        }

        public TipoCurso GetById(long id)
        {
            return _ctx.TipoCursos.FirstOrDefault(x => x.Id == id);
        }

        public TipoCurso GetByDescricao(string descricao)
        {
            return _ctx.TipoCursos.FirstOrDefault(x => x.Descricao == descricao);
        }

        public List<TipoCurso> CreateRange(List<TipoCurso> tipoCursos)
        {
            _ctx.AddRange(tipoCursos);

            return tipoCursos;
        }
    }
}
