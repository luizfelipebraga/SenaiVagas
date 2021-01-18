using Remotion.Linq.Clauses.ResultOperators;
using Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate;
using Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class ConviteEntrevistaRepository : IConviteEntrevistaRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public ConviteEntrevistaRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public ConviteEntrevista Create(ConviteEntrevista objeto)
        {
            return _ctx.ConviteEntrevistas.Add(objeto).Entity;
        }

        public ConviteEntrevista GetById(long id)
        {
            return _ctx.ConviteEntrevistas.FirstOrDefault(x => x.Id == id);
        }

        public ConviteEntrevista GetConviteEntrevistasByVagaIdAndCandidatoId(long vagaId, long usuarioCandidatoId)
        {
            return _ctx.ConviteEntrevistas.FirstOrDefault(x => x.VagaId == vagaId && x.UsuarioCandidatoAlunoId == usuarioCandidatoId);
        }
    }
}
