using Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate
{
    public interface IValidacaoUsuarioCandidatoRepository : IRepository<ValidacaoUsuarioCandidato>
    {
        ValidacaoUsuarioCandidato GetValidacaoUsuarioCandidatoAtualByAlunoId(long AlunoId);
        ValidacaoUsuarioCandidato GetValidacaoUsuarioCandidatoByToken(string Token);
        ValidacaoUsuarioCandidato UpdateValidacaoUsuarioCandidato(ValidacaoUsuarioCandidato validacaoUsuarioCandidato);
    }
}
