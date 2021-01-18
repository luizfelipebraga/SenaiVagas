using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class HistoricoStatusUsuarioViewModel
    {
        public long Id { get; set; }
        public bool Atual { get; set; }
        public StatusUsuarioViewModel StatusUsuario { get; set; }
        public UsuarioViewModel Usuario { get; set; }
    }
}
