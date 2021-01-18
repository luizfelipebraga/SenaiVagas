using Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate;
using Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class RequerimentoMatriculaRepository : IRequerimentoMatriculaRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public RequerimentoMatriculaRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public RequerimentoMatricula Create(RequerimentoMatricula objeto)
        {
            return _ctx.RequerimentoMatriculas.Add(objeto).Entity;
        }

        public RequerimentoMatricula GetById(long id)
        {
            return _ctx.RequerimentoMatriculas.FirstOrDefault(x => x.Id == id);
        }

        public RequerimentoMatricula GetByDescricao(string descricao)
        {
            return _ctx.RequerimentoMatriculas.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
