using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public class StatusUsuarioDefaultValuesAcess
    {
        public static string GetValue(StatusUsuarioDefaultValues statusUsuarioDefaultValues)
        {
            string descricao = null;

            switch (statusUsuarioDefaultValues)
            {
                case StatusUsuarioDefaultValues.ContaAtiva:
                    descricao = "Conta Ativa";
                    break;
                case StatusUsuarioDefaultValues.ContaDesativada:
                    descricao = "Conta Desativada";
                    break;
                case StatusUsuarioDefaultValues.ContaExcluida:
                    descricao = "Conta Excluída";
                    break;
            }

            return descricao;

        }
    }

    public enum StatusUsuarioDefaultValues
    {
        ContaAtiva,
        ContaDesativada,
        ContaExcluida
    }
}
