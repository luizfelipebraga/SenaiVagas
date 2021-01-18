using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models
{
    public class AtividadeCnae : AbstractDomain
    {
        public long TipoCnaeId { get; set; }
        public long TipoAtividadeCnaeId { get; set; }
        public long EmpresaId { get; set; }

        public AtividadeCnae(long tipoCnaeId, long tipoAtividadeCnaeId, long empresaId)
        {
            TipoCnaeId = tipoCnaeId;
            TipoAtividadeCnaeId = tipoAtividadeCnaeId;
            EmpresaId = empresaId;
        }
    }
}
