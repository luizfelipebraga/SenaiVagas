using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public class TipoAtividadeCnaeDefaultValuesAccess
    {
        public static string GetValue(TipoAtividadeCnaeDefaultValues tipoAtividadeCnaeDefaultValues)
        {
            string retorno = null;
            switch (tipoAtividadeCnaeDefaultValues)
            {
                case TipoAtividadeCnaeDefaultValues.AtividadePrincipal:
                    retorno = "Atividade Principal";
                    break;

                case TipoAtividadeCnaeDefaultValues.AtividadeSecundaria:
                    retorno = "Atividade Secundaria";
                    break;

            }

            return retorno;
        }
    }

    public enum TipoAtividadeCnaeDefaultValues
    {
        AtividadePrincipal,
        AtividadeSecundaria
    }
}
