using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models
{
    public class PerfilUsuarioCandidatoAluno : AbstractDomain
    {
        public string LinkExterno { get; set; }
        public string SobreOCandidato { get; set; }
        public long UsuarioCandidatoAlunoId { get; set; }

        public PerfilUsuarioCandidatoAluno(string linkExterno, string sobreOCandidato, long usuarioCandidatoAlunoId)
        {
            LinkExterno = linkExterno;
            SobreOCandidato = sobreOCandidato;
            UsuarioCandidatoAlunoId = usuarioCandidatoAlunoId;
        }

        public void AlterarInformacoes(string linkExterno, string sobreOCandidato)
        {
            LinkExterno = linkExterno;
            SobreOCandidato = sobreOCandidato;
        }
    }
}
