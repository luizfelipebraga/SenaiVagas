using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public static class RequerimentoMatriculaDefaultValuesAccess
    {
        public static string GetValue(RequerimentoMatriculaDefaultValues requerimentoMatriculaDefaultValues)
        {
            string descricao = null;
            switch (requerimentoMatriculaDefaultValues)
            {
                case RequerimentoMatriculaDefaultValues.Assinatura:
                    descricao = "Assinatura";
                    break;
                case RequerimentoMatriculaDefaultValues.Nao:
                    descricao = "Não";
                    break;
                case RequerimentoMatriculaDefaultValues.Sim:
                    descricao = "Sim";
                    break;
            }
            return descricao;
        }
    }

    public enum RequerimentoMatriculaDefaultValues
    {
        Sim,
        Nao,
        Assinatura
    }
}
