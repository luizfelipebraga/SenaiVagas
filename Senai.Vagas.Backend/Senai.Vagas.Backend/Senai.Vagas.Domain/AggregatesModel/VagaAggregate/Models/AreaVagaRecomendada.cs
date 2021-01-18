using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.VagaAggregate.Models
{
   public class AreaVagaRecomendada : AbstractDomain
    {
        public bool Ativo { get; set; }
        public long AreaId { get; set; }
        public long VagaId { get; set; }

        public AreaVagaRecomendada(long areaId, long vagaId)
        {
            AreaId = areaId;
            VagaId = vagaId;
            Ativo = true;
        }

        public void AlterarParaAntigo()
        {
            Ativo = false;
        }

        public void AlterarParaAtivo()
        {
            Ativo = true;
        }
    }
}
