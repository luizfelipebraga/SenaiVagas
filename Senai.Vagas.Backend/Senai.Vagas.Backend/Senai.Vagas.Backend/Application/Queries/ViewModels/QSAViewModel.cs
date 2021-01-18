using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class QSAViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Qualificacao { get; set; }
        public long EmpresaId { get; set; }
    }
}
