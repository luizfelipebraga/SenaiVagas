using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Usuario GetUsuarioByEmail(string email);
        Usuario Update(Usuario usuario);
    }
}
