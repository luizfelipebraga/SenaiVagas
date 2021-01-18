using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public class TipoExperienciaDefaultValuesAccess
    {
        public static string GetValue(TipoExperienciaDefaultValues tipoExperienciaDefaultValues)
        {
            string descricao = null;

            switch (tipoExperienciaDefaultValues)
            {
                case TipoExperienciaDefaultValues.Junior:
                    descricao = "Junior";
                    break;
                case TipoExperienciaDefaultValues.Senior:
                    descricao = "Sênior";
                    break;
                case TipoExperienciaDefaultValues.Pleno:
                    descricao = "Pleno";
                    break;
                case TipoExperienciaDefaultValues.NaoRequerido:
                    descricao = "Não requerido";
                    break;
            }

            return descricao;
        }
    }


    public enum TipoExperienciaDefaultValues
    {
        Junior,
        Senior,
        Pleno,
        NaoRequerido
    }
}





