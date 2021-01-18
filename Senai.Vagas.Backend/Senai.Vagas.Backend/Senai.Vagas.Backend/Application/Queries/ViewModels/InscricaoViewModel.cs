using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class InscricaoViewModel
    {
        public long Id { get; set; }
        public string NomeAluno { get; set; }
        public bool Ativo { get; set; }
        public bool RecebeuConvite { get; set; }
        public UsuarioCandidatoAlunoViewModel UsuarioCandidatoAluno { get; set; }
        public long VagaId { get; set; }
    }
}
