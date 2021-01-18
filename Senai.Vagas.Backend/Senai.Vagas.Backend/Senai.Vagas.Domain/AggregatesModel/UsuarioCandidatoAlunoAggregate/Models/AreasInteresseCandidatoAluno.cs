using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models
{
    public class AreasInteresseCandidatoAluno : AbstractDomain
    {
        public bool Ativo { get; set; }
        public long AreaId { get; set; }
        public long UsuarioCandidatoAlunoId { get; set; }

        public AreasInteresseCandidatoAluno(long areaId, long usuarioCandidatoAlunoId)
        {
            AreaId = areaId;
            UsuarioCandidatoAlunoId = usuarioCandidatoAlunoId;
            Ativo = true;
        }

        public void AlterarParaAntigo()
        {
            Ativo = false;
        }

        public void AlterarParaAtivo()
        {
            Ativo = true;
        }
    }
}
