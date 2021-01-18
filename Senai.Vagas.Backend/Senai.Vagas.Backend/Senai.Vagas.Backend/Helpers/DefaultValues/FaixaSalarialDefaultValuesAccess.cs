using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public class FaixaSalarialDefaultValuesAccess
    {
        public static string GetValue(FaixaSalarialDefaultValues faixaSalarialDefaultValues)
        {
            string descricao = null;

            switch (faixaSalarialDefaultValues)
            {
                case FaixaSalarialDefaultValues.Menosde1000:
                    descricao = "Menos de R$ 1000,00";
                    break;
                case FaixaSalarialDefaultValues.Entre1000e2000:
                    descricao = "Entre R$ 1000,00 e R$ 2000,00";
                    break;
                case FaixaSalarialDefaultValues.Entre2000e3000:
                    descricao = "Entre R$ 2000,00 e R$ 3000,00";
                    break;
                case FaixaSalarialDefaultValues.Maisde3000:
                    descricao = "Mais de R$ 3000,00";
                    break;
                case FaixaSalarialDefaultValues.ACombinar:
                    descricao = "A Combinar";
                    break;
            }

            return descricao;
        }
    }

    public enum FaixaSalarialDefaultValues
    {
        Menosde1000,
        Entre1000e2000,
        Entre2000e3000,
        Maisde3000,
        ACombinar
    }
}
