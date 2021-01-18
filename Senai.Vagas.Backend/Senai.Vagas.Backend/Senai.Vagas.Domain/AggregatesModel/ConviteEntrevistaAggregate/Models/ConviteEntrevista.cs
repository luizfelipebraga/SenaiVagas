using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate.Models
{
   public class ConviteEntrevista : AbstractDomain
    {
        public DateTime DataHoraEntrevista { get; set; }
        public string InfosComplementares { get; set; }
        public long EnderecoId { get; set; }
        public long UsuarioCandidatoAlunoId { get; set; }
        public long VagaId { get; set; }

        public ConviteEntrevista(DateTime dataHoraEntrevista, string infosComplementares, long enderecoId, long usuarioCandidatoAlunoId, long vagaId)
        {
            DataHoraEntrevista = dataHoraEntrevista;
            InfosComplementares = infosComplementares;
            EnderecoId = enderecoId;
            UsuarioCandidatoAlunoId = usuarioCandidatoAlunoId;
            VagaId = vagaId;
        }
    }
}
