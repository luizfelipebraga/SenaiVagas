using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class StatusVagaJobs : IJobs
    {
        public IStatusVagaRepository _statusVagaRepository;

        public StatusVagaJobs(IStatusVagaRepository statusVagaRepository)
        {
            _statusVagaRepository = statusVagaRepository;
        }

        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 3; i++)
            {
                var defaultValue = (StatusVagaDefaultValues)i;
                var stringDefaultValue = StatusVagaDefaultValuesAcess.GetValue(defaultValue);

                var statusVagaDb = _statusVagaRepository.GetByDescricao(stringDefaultValue);

                if (statusVagaDb == null)
                {
                    statusVagaDb = new StatusVaga(stringDefaultValue);

                    _statusVagaRepository.Create(statusVagaDb);
                    await _statusVagaRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
