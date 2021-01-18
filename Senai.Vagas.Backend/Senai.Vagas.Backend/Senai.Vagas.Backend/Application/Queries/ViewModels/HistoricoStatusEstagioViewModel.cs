using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class HistoricoStatusEstagioViewModel
    {
        public long Id { get; set; }
        public string Explicacao { get; set; }
        public bool Atual { get; set; }
        public StatusEstagioViewModel StatusEstagio { get; set; }
        public EstagioViewModel Estagio { get; set; }
    }
}
