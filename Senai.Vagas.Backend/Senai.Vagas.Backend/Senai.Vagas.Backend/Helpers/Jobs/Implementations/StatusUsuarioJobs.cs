using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class StatusUsuarioJobs : IJobs
    {
        public IStatusUsuarioRepository _statusUsuarioRepository;

        public StatusUsuarioJobs(IStatusUsuarioRepository statusUsuarioRepository)
        {
            _statusUsuarioRepository = statusUsuarioRepository;
        }
        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 3; i++)
            {
                var defaultValue = (StatusUsuarioDefaultValues)i;
                var stringDefaultValue = StatusUsuarioDefaultValuesAcess.GetValue(defaultValue);

                var statusUsuarioDb = _statusUsuarioRepository.GetByDescricao(stringDefaultValue);

                if (statusUsuarioDb == null)
                {
                    statusUsuarioDb = new StatusUsuario(stringDefaultValue);

                    _statusUsuarioRepository.Create(statusUsuarioDb);
                    await _statusUsuarioRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
