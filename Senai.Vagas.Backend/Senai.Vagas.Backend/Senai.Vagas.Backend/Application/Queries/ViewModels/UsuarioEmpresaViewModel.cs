using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class UsuarioEmpresaViewModel
    {
        public long Id { get; set; }
        public EmpresaViewModel Empresa { get; set; }
        public UsuarioViewModel Usuario{ get; set; }

    }
}
