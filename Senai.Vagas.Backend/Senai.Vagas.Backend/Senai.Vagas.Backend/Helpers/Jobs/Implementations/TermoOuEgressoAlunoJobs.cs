using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class TermoOuEgressoAlunoJobs : IJobs
    {
        public ITermoOuEgressoAlunoRepository _termoOuEgressoAlunoRepository;

        public TermoOuEgressoAlunoJobs(ITermoOuEgressoAlunoRepository termoOuEgressoAlunoRepository)
        {
            _termoOuEgressoAlunoRepository = termoOuEgressoAlunoRepository;
        }
        public async Task ExecuteAsync()
        {
            for (int i = 1; i < 6; i++)
            {
                var defaultValue = (TermoOuEgressoAlunoDefaultValues)i;
                var stringDefaultValue = TermoOuEgressoAlunoDefaultValuesAccess.GetValue(defaultValue);

                var termoOuEgressoAlunoDb = _termoOuEgressoAlunoRepository.GetByDescricao(stringDefaultValue);

                if (termoOuEgressoAlunoDb == null)
                {
                    termoOuEgressoAlunoDb = new TermoOuEgressoAluno(stringDefaultValue);

                    _termoOuEgressoAlunoRepository.Create(termoOuEgressoAlunoDb);
                    await _termoOuEgressoAlunoRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
