using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate;
using Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class RequerimentoMatriculaJobs : IJobs
    {
        public IRequerimentoMatriculaRepository _requerimentoMatriculaRepository;

        public RequerimentoMatriculaJobs(IRequerimentoMatriculaRepository requerimentoMatriculaRepository)
        {
            _requerimentoMatriculaRepository = requerimentoMatriculaRepository;
        }

        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 3; i++)
            {
                var defaultValue = (RequerimentoMatriculaDefaultValues)i;
                var stringDefaultValue = RequerimentoMatriculaDefaultValuesAccess.GetValue(defaultValue);

                var requerimentoMatriculaDb = _requerimentoMatriculaRepository.GetByDescricao(stringDefaultValue);

                if (requerimentoMatriculaDb == null)
                {
                    requerimentoMatriculaDb = new RequerimentoMatricula(stringDefaultValue);

                    _requerimentoMatriculaRepository.Create(requerimentoMatriculaDb);
                    await _requerimentoMatriculaRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
