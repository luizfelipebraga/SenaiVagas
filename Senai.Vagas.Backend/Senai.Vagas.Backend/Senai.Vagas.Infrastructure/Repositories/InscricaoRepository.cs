using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate;
using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class InscricaoRepository : IInscricaoRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public InscricaoRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public Inscricao Create(Inscricao objeto)
        {
            return _ctx.Inscricoes.Add(objeto).Entity;
        }

        public Inscricao GetById(long id)
        {
            return _ctx.Inscricoes.FirstOrDefault(x => x.Id == id);
        }

        public List<Inscricao> GetAllInscricoesByVagaId(long vagaId)
        {
            return _ctx.Inscricoes.Where(x => x.VagaId == vagaId).ToList();
        }

        public Inscricao GetInscricaoByVagaIdAndCandidatoId(long vagaId, long usuarioCandidatoAlunoId)
        {
            return _ctx.Inscricoes.FirstOrDefault(x => x.VagaId == vagaId && x.UsuarioCandidatoAlunoId == usuarioCandidatoAlunoId);
        }

        public Inscricao UpdateInscricao(Inscricao inscricao)
        {
            return _ctx.Inscricoes.Update(inscricao).Entity;
        }

        public Inscricao GetInscricaoByVagaIdAndCandidatoIdAtual(long vagaId, long usuarioCandidatoAlunoId)
        {
            return _ctx.Inscricoes.FirstOrDefault(x => x.VagaId == vagaId && x.UsuarioCandidatoAlunoId == usuarioCandidatoAlunoId && x.Ativo == true);
        }
    }
}
