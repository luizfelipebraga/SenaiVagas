using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate;
using Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class FaixaSalarialJobs : IJobs
    {
        public IFaixaSalarialRepository _faixaSalarialRepository;

        public FaixaSalarialJobs(IFaixaSalarialRepository faixaSalarialRepository)
        {
            _faixaSalarialRepository = faixaSalarialRepository;
        }

        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 5; i++)
            {
                var defaultValue = (FaixaSalarialDefaultValues)i;
                var stringDefaultValue = FaixaSalarialDefaultValuesAccess.GetValue(defaultValue);

                var faixaSalarialDb = _faixaSalarialRepository.GetByDescricao(stringDefaultValue);

                if (faixaSalarialDb == null)
                {
                    faixaSalarialDb = new FaixaSalarial(stringDefaultValue);

                    _faixaSalarialRepository.Create(faixaSalarialDb);
                    await _faixaSalarialRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
