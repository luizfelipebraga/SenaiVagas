using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public static class StatusVagaDefaultValuesAcess
    {
        public static string GetValue(StatusVagaDefaultValues statusVagaDefaultValues)
        {
            string descricao = null;

            switch (statusVagaDefaultValues)
            {
                case StatusVagaDefaultValues.VagaAtiva:
                    descricao = "Vaga Ativa";
                    break;
                case StatusVagaDefaultValues.VagaEncerrada:
                    descricao = "Vaga Encerrada";
                    break;
                case StatusVagaDefaultValues.VagaExcluida:
                    descricao = "Vaga Excluida";
                    break;
            }

            return descricao;
        }
    }

    public enum StatusVagaDefaultValues
    {
        VagaAtiva,
        VagaEncerrada,
        VagaExcluida
    }
}
