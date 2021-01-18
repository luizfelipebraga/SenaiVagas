using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.DefaultValues
{
    public static class TermoOuEgressoAlunoDefaultValuesAccess
    {
        public static string GetValue(TermoOuEgressoAlunoDefaultValues TermoOuEgressoAlunoDefaultValues)
        {
            string descricao = null;

            switch (TermoOuEgressoAlunoDefaultValues)
            {
                case TermoOuEgressoAlunoDefaultValues.Egresso:
                    descricao = "Egresso";
                    break;
                case TermoOuEgressoAlunoDefaultValues.Termo1:
                    descricao = "1º Termo";
                    break;
                case TermoOuEgressoAlunoDefaultValues.Termo2:
                    descricao = "2º Termo";
                    break;
                case TermoOuEgressoAlunoDefaultValues.Termo3:
                    descricao = "3º Termo";
                    break;
                case TermoOuEgressoAlunoDefaultValues.Termo4:
                    descricao = "4º Termo";
                    break;
            }
            return descricao;
        }
    }

    public enum TermoOuEgressoAlunoDefaultValues
    {
        Termo1 = 1,
        Termo2,
        Termo3,
        Termo4,
        Egresso
    }

}

