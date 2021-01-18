using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate.Models
{
    public class Inscricao : AbstractDomain
    {
        public bool Ativo { get; set; }
        public bool RecebeuConvite { get; set; }
        public long UsuarioCandidatoAlunoId { get; set; }
        public long VagaId { get; set; }

        public Inscricao(long usuarioCandidatoAlunoId, long vagaId)
        {
            Ativo = true;
            RecebeuConvite = false;
            UsuarioCandidatoAlunoId = usuarioCandidatoAlunoId;
            VagaId = vagaId;
        }

        public void AlterarParaAntigo()
        {
            Ativo = false;
        }

        public void AlterarParaAtual()
        {
            Ativo = true;
        }

        public void AlterarConviteRecebido()
        {
            RecebeuConvite = true;
        }
    }
}
