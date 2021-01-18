using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class EstagioViewModel
    {
        public long Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTerminoPrevisto { get; set; }
        public DateTime DataTerminoEfetivo { get; set; }
        public int DiasContrato { get; set; }
        public bool Contrato { get; set; }
        public bool PlanoEstagio { get; set; }
        public bool Desligamento { get; set; }
        public RequerimentoMatriculaViewModel RequerimentoMatricula { get; set; }
        public TermoOuEgressoAlunoViewModel TermoOuEgressoAluno { get; set; }
        public PessoaResponsavelViewModel PessoaResponsavel { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public AlunoViewModel Aluno { get; set; }
        public UsuarioEmpresaViewModel UsuarioEmpresa { get; set; }
    }
}
