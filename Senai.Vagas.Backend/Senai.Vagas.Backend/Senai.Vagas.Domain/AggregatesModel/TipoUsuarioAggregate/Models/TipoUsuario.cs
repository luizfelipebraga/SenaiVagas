using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate.Models
{
    public class TipoUsuario : AbstractDomain
    {
        public string Descricao { get; set; }

        public TipoUsuario(string descricao)
        {
            Descricao = descricao;
        }
    }
}
