using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EstagioAggregate.Models
{
    public class Estagio : AbstractDomain
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataTerminoPrevisto { get; set; }
        public DateTime DataTerminoEfetivo { get; set; }
        public int DiasContrato { get; set; }
        public bool Contrato { get; set; }
        public bool PlanoEstagio { get; set; }
        public bool Desligamento { get; set; }
        public long RequerimentoMatriculaId { get; set; }
        public long TermoOuEgressoAlunoId { get; set; }
        public long PessoaResponsavelId { get; set; }
        public long EnderecoId { get; set; }
        public long AlunoId { get; set; }
        public long UsuarioEmpresaId { get; set; }

        public Estagio(DateTime dataInicio, DateTime dataTerminoPrevisto, DateTime dataTerminoEfetivo, int diasContrato, bool contrato, bool planoEstagio, bool desligamento, long requerimentoMatriculaId, long termoOuEgressoAlunoId, long pessoaResponsavelId, long enderecoId, long alunoId, long usuarioEmpresaId)
        {
            DataInicio = dataInicio;
            DataTerminoPrevisto = dataTerminoPrevisto;
            DataTerminoEfetivo = dataTerminoEfetivo;
            DiasContrato = diasContrato;
            Contrato = contrato;
            PlanoEstagio = planoEstagio;
            Desligamento = desligamento;
            RequerimentoMatriculaId = requerimentoMatriculaId;
            TermoOuEgressoAlunoId = termoOuEgressoAlunoId;
            PessoaResponsavelId = pessoaResponsavelId;
            EnderecoId = enderecoId;
            AlunoId = alunoId;
            UsuarioEmpresaId = usuarioEmpresaId;
        }
    }
}
