using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class UsuarioAdministradorRepository : IUsuarioAdministradorRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public UsuarioAdministradorRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public UsuarioAdministrador Create(UsuarioAdministrador objeto)
        {
            return _ctx.UsuarioAdministradores.Add(objeto).Entity;
        }

        public UsuarioAdministrador GetById(long id)
        {
            return _ctx.UsuarioAdministradores.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioAdministrador GetByUsuarioId(long usuarioId)
        {
            return _ctx.UsuarioAdministradores.FirstOrDefault(x => x.UsuarioId == usuarioId);
        }
    }
}
