using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.AlunoAggregate.Models
{
    public class Aluno : AbstractDomain
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string RMA { get; set; }
        public bool Sexo { get; set; } // TRUE para Masculino | FALSE para Feminino
        public DateTime DataNascimento { get; set; }
        public DateTime DataMatricula { get; set; }
        public long TermoOuEgressoAlunoId { get; set; }
        public long TipoCursoId { get; set; }

        public Aluno(string nomeCompleto, string email, string rMA, bool sexo, DateTime dataNascimento, DateTime dataMatricula, long termoOuEgressoAlunoId, long tipoCursoId)
        {
            NomeCompleto = nomeCompleto;
            Email = email;
            RMA = rMA;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            DataMatricula = dataMatricula;
            TermoOuEgressoAlunoId = termoOuEgressoAlunoId;
            TipoCursoId = tipoCursoId;
        }
    }
}
