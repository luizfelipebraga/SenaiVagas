using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.AreaAggregate;
using Senai.Vagas.Domain.AggregatesModel.AreaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class AreaJobs : IJobs
    {
        public IAreaRepository _areaRepository { get; set; }

        public AreaJobs(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 7; i++)
            {
                var defaultValue = (AreaDefaultValues)i;
                var stringDefaultValue = AreaDefaultValuesAccess.GetValue(defaultValue);

                var areaDb = _areaRepository.GetByDescricao(stringDefaultValue);

                if (areaDb == null)
                {
                    areaDb = new Area(stringDefaultValue);

                    _areaRepository.Create(areaDb);
                    await _areaRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }
    }
}
