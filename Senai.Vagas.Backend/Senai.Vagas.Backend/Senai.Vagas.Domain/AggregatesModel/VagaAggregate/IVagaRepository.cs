using Senai.Vagas.Domain.AggregatesModel.VagaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.VagaAggregate
{
    public interface IVagaRepository : IRepository<Vaga>
    {
        AreaVagaRecomendada CreateAreaVagaRecomendada(AreaVagaRecomendada areaVagaRecomendada);
        List<AreaVagaRecomendada> CreateRangeAreasVagasRecomendadas(List<AreaVagaRecomendada> areasVagasRecomendadas);
        List<AreaVagaRecomendada> GetAllAreaVagaRecomendadabyVagaId(long vagaId);
        AreaVagaRecomendada GetAreaVagaRecomendadabyVagaIdAndAreaId(long vagaId, long areaId);
        AreaVagaRecomendada UpdateAreaVagaRecomendada(AreaVagaRecomendada areaVagaRecomendada);
        Vaga UpdateVaga(Vaga vaga);
    }
}
