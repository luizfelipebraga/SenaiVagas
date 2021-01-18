using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public class TipoCursoDefaultValuesAcess
    {
        public static string GetValue(TipoCursoDefaultValues tipoCursoDefaultValues)
        {

            string descricao = null;

            switch (tipoCursoDefaultValues)
            {
                case TipoCursoDefaultValues.DesenvolvimentoDeSistemas:
                    descricao = "Técnico de Desenvolvimento de Sistemas";
                    break;
                case TipoCursoDefaultValues.RedesDeComputadores:
                    descricao = "Técnico de Redes de Computadores";
                    break;
                case TipoCursoDefaultValues.Multimidia:
                    descricao = "Técnico de Multimídia";
                    break;
            }

            return descricao;

        }
    }

    public enum TipoCursoDefaultValues
    {
        DesenvolvimentoDeSistemas,
        RedesDeComputadores,
        Multimidia,
    }

}
