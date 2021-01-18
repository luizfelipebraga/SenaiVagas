using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class PerfilUsuarioCandidatoAlunoViewModel
    {
        public long Id { get; set; }
        public string LinkExterno { get; set; }
        public string SobreOCandidato { get; set; }
        public long UsuarioCandidatoAlunoId { get; set; }
    }
}
