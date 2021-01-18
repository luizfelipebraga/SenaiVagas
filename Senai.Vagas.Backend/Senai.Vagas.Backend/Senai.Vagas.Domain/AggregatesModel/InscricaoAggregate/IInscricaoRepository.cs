using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate
{
    public interface IInscricaoRepository : IRepository<Inscricao>
    {
        List<Inscricao> GetAllInscricoesByVagaId(long vagaId);
        Inscricao GetInscricaoByVagaIdAndCandidatoId(long vagaId, long usuarioCandidatoAlunoId);
        Inscricao GetInscricaoByVagaIdAndCandidatoIdAtual(long vagaId, long usuarioCandidatoAlunoId);
        Inscricao UpdateInscricao(Inscricao inscricao);
    }
}
