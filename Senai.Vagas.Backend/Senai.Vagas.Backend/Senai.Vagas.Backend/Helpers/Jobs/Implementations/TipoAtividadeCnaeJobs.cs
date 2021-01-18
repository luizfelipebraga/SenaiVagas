using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class TipoAtividadeCnaeJobs : IJobs
    {
        public ITipoAtividadeCnaeRepository _tipoAtividadeCnaeRepository;

        public TipoAtividadeCnaeJobs(ITipoAtividadeCnaeRepository tipoAtividadeCnaeRepository)
        {
            _tipoAtividadeCnaeRepository = tipoAtividadeCnaeRepository;
        }

        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 2; i++)
            {
                var defaultValue = (TipoAtividadeCnaeDefaultValues)i;
                var stringDefaultValue = TipoAtividadeCnaeDefaultValuesAccess.GetValue(defaultValue);

                var tipoAtividadeCnaeDb = _tipoAtividadeCnaeRepository.GetByDescricao(stringDefaultValue);

                if (tipoAtividadeCnaeDb == null)
                {
                    tipoAtividadeCnaeDb = new TipoAtividadeCnae(stringDefaultValue);

                    _tipoAtividadeCnaeRepository.Create(tipoAtividadeCnaeDb);
                    await _tipoAtividadeCnaeRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
