using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusEstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusEstagioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class HistoricoStatusEstagioRepository : IHistoricoStatusEstagioRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public HistoricoStatusEstagioRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }
        public HistoricoStatusEstagio Create(HistoricoStatusEstagio objeto)
        {
            return _ctx.HistoricoStatusEstagios.Add(objeto).Entity;
        }

        public HistoricoStatusEstagio GetById(long id)
        {
            return _ctx.HistoricoStatusEstagios.FirstOrDefault(x => x.Id == id);
        }

        public HistoricoStatusEstagio GetHistoricoAtualByEstagioId(long estagioId)
        {
            return _ctx.HistoricoStatusEstagios.FirstOrDefault(x => x.EstagioId == estagioId && x.Atual);
            
        }
    }
}
