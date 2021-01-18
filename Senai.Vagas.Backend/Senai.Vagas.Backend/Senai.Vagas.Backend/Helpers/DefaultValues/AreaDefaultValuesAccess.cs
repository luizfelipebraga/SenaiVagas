using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public class AreaDefaultValuesAccess
    {
        public static string GetValue(AreaDefaultValues areaDefaultValues)
        {
            string descricao = null;

            switch (areaDefaultValues)
            {
                case AreaDefaultValues.WebDesign:
                    descricao = "Web Design";
                    break;
                case AreaDefaultValues.DesenvolvimentoFrontend:
                    descricao = "Desenvolvedor Frontend";
                    break;
                case AreaDefaultValues.DesenvolvedorBackend:
                    descricao = "Desenvolvedor de Backend";
                    break;
                case AreaDefaultValues.SuporteTecnico:
                    descricao = "Suporte Técnico";
                    break;
                case AreaDefaultValues.Multimidia:
                    descricao = "Multimídia";
                    break;
                case AreaDefaultValues.RedesInformatica:
                    descricao = "Redes de Informática";
                    break;
                case AreaDefaultValues.DesignGrafico:
                    descricao = "Design Gráfico";
                    break;
            }

            return descricao;
        }
    }

    public enum AreaDefaultValues
    {
        WebDesign,
        DesenvolvimentoFrontend,
        DesenvolvedorBackend,
        SuporteTecnico,
        Multimidia,
        RedesInformatica,
        DesignGrafico
    }
}
