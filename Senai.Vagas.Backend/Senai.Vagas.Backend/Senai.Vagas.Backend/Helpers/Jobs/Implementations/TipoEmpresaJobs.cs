using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class TipoEmpresaJobs : IJobs
    {
        public ITipoEmpresaRepository _tipoEmpresaRepository;

        public TipoEmpresaJobs(ITipoEmpresaRepository tipoEmpresaRepository)
        {
            _tipoEmpresaRepository = tipoEmpresaRepository;
        }
        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 2; i++)
            {
                var defaultValue = (TipoEmpresaDefaultValues)i;
                var stringDefaultValue = TipoEmpresaDefaultValuesAccess.GetValue(defaultValue);

                var tipoEmpresaDb = _tipoEmpresaRepository.GetByDescricao(stringDefaultValue);

                if (tipoEmpresaDb == null)
                {
                    tipoEmpresaDb = new TipoEmpresa(stringDefaultValue);

                    _tipoEmpresaRepository.Create(tipoEmpresaDb);
                    await _tipoEmpresaRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
