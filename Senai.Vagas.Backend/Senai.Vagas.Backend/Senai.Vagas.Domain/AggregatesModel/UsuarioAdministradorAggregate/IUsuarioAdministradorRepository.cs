using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate
{
    public interface IUsuarioAdministradorRepository : IRepository<UsuarioAdministrador>
    {
        UsuarioAdministrador GetByUsuarioId(long usuarioId);
    }
}
