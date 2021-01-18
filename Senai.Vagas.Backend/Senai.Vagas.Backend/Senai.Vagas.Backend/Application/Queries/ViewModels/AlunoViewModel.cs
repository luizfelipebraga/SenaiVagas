using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class AlunoViewModel
    {
        public long Id { get; set; }
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string RMA { get; set; }
        public bool Sexo { get; set; } // TRUE para Masculino | FALSE para Feminino
        public DateTime DataNascimento { get; set; }
        public DateTime DataMatricula { get; set; }
        public TermoOuEgressoAlunoViewModel TermoOuEgressoAluno{ get; set; }
        public TipoCursoViewModel TipoCurso{ get; set; }
        public PerfilUsuarioCandidatoAlunoViewModel PerfilCandidato { get; set; }
    }
}
