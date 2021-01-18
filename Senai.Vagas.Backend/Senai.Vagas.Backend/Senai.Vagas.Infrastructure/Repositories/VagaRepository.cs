using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class VagaRepository : IVagaRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public VagaRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public Vaga Create(Vaga objeto)
        {
            return _ctx.Vagas.Add(objeto).Entity;
        }

        public AreaVagaRecomendada CreateAreaVagaRecomendada(AreaVagaRecomendada areaVagaRecomendada)
        {
            return _ctx.AreaVagaRecomendadas.Add(areaVagaRecomendada).Entity;
        }

        public List<AreaVagaRecomendada> GetAllAreaVagaRecomendadabyVagaId(long vagaId)
        {

            return _ctx.AreaVagaRecomendadas.Where(x => x.VagaId == vagaId && x.Ativo).ToList();
        }

        public Vaga GetById(long id)
        {
            return _ctx.Vagas.FirstOrDefault(x => x.Id == id);
        }

        public List<AreaVagaRecomendada> CreateRangeAreasVagasRecomendadas(List<AreaVagaRecomendada> areasVagasRecomendadas)
        {
            _ctx.AreaVagaRecomendadas.AddRange(areasVagasRecomendadas);

            return areasVagasRecomendadas;
        }

        public AreaVagaRecomendada GetAreaVagaRecomendadabyVagaIdAndAreaId(long vagaId, long areaId)
        {
            return _ctx.AreaVagaRecomendadas.FirstOrDefault(x => x.VagaId == vagaId && x.AreaId == areaId);
        }

        public AreaVagaRecomendada UpdateAreaVagaRecomendada(AreaVagaRecomendada areaVagaRecomendada)
        {
            return _ctx.AreaVagaRecomendadas.Update(areaVagaRecomendada).Entity;
        }

        public Vaga UpdateVaga(Vaga vaga)
        {
            return _ctx.Vagas.Update(vaga).Entity;
        }
    }
}
