using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public class StatusEstagioDefaultValuesAcess
    {
        public static string GetValue(StatusEstagioDefaultValues statusEstagioDefaultValues)
        {
            string descricao = null;

            switch (statusEstagioDefaultValues)
            {
                case StatusEstagioDefaultValues.Concluido:
                    descricao = "Concluido";
                    break;
                case StatusEstagioDefaultValues.EmAndamento:
                    descricao = "Em Andamento";
                    break;
                case StatusEstagioDefaultValues.Evadido:
                    descricao = "Evadido";
                    break;
            }

            return descricao;
        }
    }

    public enum StatusEstagioDefaultValues
    {
        Concluido,
        EmAndamento,
        Evadido
    }
}

