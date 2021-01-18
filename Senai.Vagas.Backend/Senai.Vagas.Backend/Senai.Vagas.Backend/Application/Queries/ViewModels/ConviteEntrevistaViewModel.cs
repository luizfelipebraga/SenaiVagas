using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class ConviteEntrevistaViewModel
    {
        public long Id { get; set; }
        public DateTime DataHoraEntrevista { get; set; }
        public string InfosComplementares { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public long UsuarioCandidatoAlunoId { get; set; }
        public long VagaId { get; set; }
    }
}
