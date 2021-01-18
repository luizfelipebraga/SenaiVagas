using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class TipoExperienciaJobs : IJobs
    {
        public ITipoExperienciaRepository _tipoExperienciaRepository;
        public TipoExperienciaJobs(ITipoExperienciaRepository tipoExperienciaRepository)
        {
            _tipoExperienciaRepository = tipoExperienciaRepository;
        }
        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 4; i++)
            {
                var defaultValue = (TipoExperienciaDefaultValues)i;
                var stringDefaultValue = TipoExperienciaDefaultValuesAccess.GetValue(defaultValue);

                var tipoExperienciaDb = _tipoExperienciaRepository.GetByDescricao(stringDefaultValue);

                if (tipoExperienciaDb == null)
                {
                    tipoExperienciaDb = new TipoExperiencia(stringDefaultValue);

                    _tipoExperienciaRepository.Create(tipoExperienciaDb);
                    await _tipoExperienciaRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
