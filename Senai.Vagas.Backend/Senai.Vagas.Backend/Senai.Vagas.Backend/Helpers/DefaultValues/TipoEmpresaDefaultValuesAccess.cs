using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public class TipoEmpresaDefaultValuesAccess
    {
        public static string GetValue(TipoEmpresaDefaultValues tipoEmpresaDefaultValues)
        {
            string retorno = null;
            switch (tipoEmpresaDefaultValues)
            {
                case TipoEmpresaDefaultValues.FILIAL:
                    retorno = "FILIAL";
                    break;
                case TipoEmpresaDefaultValues.MATRIZ:
                    retorno = "MATRIZ";
                    break;          
            }

         
            return retorno;
        }
    }
    public enum TipoEmpresaDefaultValues 
    {
        FILIAL,
        MATRIZ
    }
}
