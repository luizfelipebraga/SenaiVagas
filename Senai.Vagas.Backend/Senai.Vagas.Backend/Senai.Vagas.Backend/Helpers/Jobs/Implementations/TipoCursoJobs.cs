using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Jobs.Implementations
{
    public class TipoCursoJobs : IJobs
    {
        public ITipoCursoRepository _tipoCursoRepository;

        public TipoCursoJobs(ITipoCursoRepository tipoCursoRepository)
        {
            _tipoCursoRepository = tipoCursoRepository;
        }

        public async Task ExecuteAsync()
        {
            for (int i = 0; i < 3; i++)
            {
                var defaultValue = (TipoCursoDefaultValues)i;
                var stringDefaultValue = TipoCursoDefaultValuesAcess.GetValue(defaultValue);

                var tipoCursoDb = _tipoCursoRepository.GetByDescricao(stringDefaultValue);

                if (tipoCursoDb == null)
                {
                    tipoCursoDb = new TipoCurso(stringDefaultValue);

                    _tipoCursoRepository.Create(tipoCursoDb);
                    await _tipoCursoRepository.UnitOfWork.SaveDbChanges();
                }
            }
        }

    }
}
