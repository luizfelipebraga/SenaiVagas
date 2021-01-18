using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public static class TipoUsuarioDefaultValuesAccess
    {
        public static string GetValue(TipoUsuarioDefaultValues tipoUsuarioDefaultValues)
        {
            string descricao = null;

            switch (tipoUsuarioDefaultValues)
            {
                case TipoUsuarioDefaultValues.Candidato:
                    descricao = "Candidato";
                    break;
                case TipoUsuarioDefaultValues.Empresa:
                    descricao = "Empresa";
                    break;
                case TipoUsuarioDefaultValues.Administrador:
                    descricao = "Administrador";
                    break;
            }
            return descricao;
        }
    }

    public enum TipoUsuarioDefaultValues
    {
        Candidato,
        Empresa,
        Administrador
    }
}
