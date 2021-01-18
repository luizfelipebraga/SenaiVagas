using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class UsuarioViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public TipoUsuarioViewModel TipoUsuario { get; set; }
    }
}
