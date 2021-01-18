using Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate;
using Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class ValidacaoUsuarioCandidatoRepository : IValidacaoUsuarioCandidatoRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public ValidacaoUsuarioCandidatoRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public ValidacaoUsuarioCandidato Create(ValidacaoUsuarioCandidato objeto)
        {
            return _ctx.ValidacaoUsuarioCandidatos.Add(objeto).Entity;
        }

        public ValidacaoUsuarioCandidato GetById(long id)
        {
            return _ctx.ValidacaoUsuarioCandidatos.FirstOrDefault(x => x.Id == id);
        }

        public ValidacaoUsuarioCandidato GetValidacaoUsuarioCandidatoAtualByAlunoId(long AlunoId)
        {
            return _ctx.ValidacaoUsuarioCandidatos.FirstOrDefault(x => x.AlunoId == AlunoId && x.Ativo);
        }

        public ValidacaoUsuarioCandidato GetValidacaoUsuarioCandidatoByToken(string Token)
        {
            return _ctx.ValidacaoUsuarioCandidatos.FirstOrDefault(x => x.Token == Token && x.Ativo);
        }

        public ValidacaoUsuarioCandidato UpdateValidacaoUsuarioCandidato(ValidacaoUsuarioCandidato validacaoUsuarioCandidato)
        {
            return _ctx.ValidacaoUsuarioCandidatos.Update(validacaoUsuarioCandidato).Entity;
        }
    }
}
