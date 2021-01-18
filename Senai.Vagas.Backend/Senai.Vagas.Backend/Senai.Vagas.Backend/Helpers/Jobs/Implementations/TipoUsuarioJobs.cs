using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class TipoUsuarioJobs : IJobs
    {
        public ITipoUsuarioRepository _tipoUsuarioRepository;

        public TipoUsuarioJobs(ITipoUsuarioRepository tipoUsuarioRepository)
        {
            _tipoUsuarioRepository = tipoUsuarioRepository;
        }

        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 3; i++)
            {
                var defaultValue = (TipoUsuarioDefaultValues)i;
                var stringDefaultValue = TipoUsuarioDefaultValuesAccess.GetValue(defaultValue);

                var tipoUsuarioDb = _tipoUsuarioRepository.GetByDescricao(stringDefaultValue);

                if (tipoUsuarioDb == null)
                {
                    tipoUsuarioDb = new TipoUsuario(stringDefaultValue);

                    _tipoUsuarioRepository.Create(tipoUsuarioDb);
                    await _tipoUsuarioRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
