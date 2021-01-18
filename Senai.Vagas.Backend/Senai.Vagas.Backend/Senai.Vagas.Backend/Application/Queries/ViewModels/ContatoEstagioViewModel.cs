using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class ContatoEstagioViewModel
    {
        public long Id { get; set; }
        public string TelefoneOuEmail { get; set; }
        public EstagioViewModel Estagio { get; set; }
    }
}
