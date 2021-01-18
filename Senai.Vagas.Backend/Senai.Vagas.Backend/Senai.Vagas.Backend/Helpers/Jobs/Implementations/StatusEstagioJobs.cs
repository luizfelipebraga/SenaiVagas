using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class StatusEstagioJobs : IJobs
    {
        public IStatusEstagioRepository _statusEstagioRepository;

        public StatusEstagioJobs(IStatusEstagioRepository statusEstagioRepository)
        {
            _statusEstagioRepository = statusEstagioRepository;
        }
        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 3; i++)
            {
                var defaultValue = (StatusEstagioDefaultValues)i;
                var stringDefaultValue = StatusEstagioDefaultValuesAcess.GetValue(defaultValue);

                var statusEstagioDb = _statusEstagioRepository.GetByDescricao(stringDefaultValue);

                if (statusEstagioDb == null)
                {
                    statusEstagioDb = new StatusEstagio(stringDefaultValue);

                    _statusEstagioRepository.Create(statusEstagioDb);
                    await _statusEstagioRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
