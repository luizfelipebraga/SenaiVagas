using Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate
{
   public interface IConviteEntrevistaRepository : IRepository<ConviteEntrevista>
    {
        ConviteEntrevista GetConviteEntrevistasByVagaIdAndCandidatoId (long vagaId, long usuarioCandidatoId);
    }
}
