using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class AtividadeCnaeViewModel
    {
        public long Id { get; set; }
        public TipoCnaeViewModel  TipoCnae { get; set; }
        public TipoAtividadeCnaeViewModel TipoAtividadeCnae { get; set; }
        public long EmpresaId { get; set; }
    }
}
