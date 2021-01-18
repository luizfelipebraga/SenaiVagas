using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class AreaVagaRecomendadaViewModel
    {
        public long Id { get; set; }
        public bool Ativo { get; set; }
        public AreaViewModel Area { get; set; }
        public long VagaId { get; set; }
    }
}
