using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models
{
    public class UsuarioCandidatoAluno : AbstractDomain
    {
        public long AlunoId { get; set; }
        public long UsuarioId { get; set; }

        public UsuarioCandidatoAluno(long alunoId, long usuarioId)
        {
            AlunoId = alunoId;
            UsuarioId = usuarioId;
        }
    }
}
