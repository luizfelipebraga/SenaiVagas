using Microsoft.EntityFrameworkCore;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public UsuarioRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public Usuario Create(Usuario objeto)
        {
            return _ctx.Usuarios.Add(objeto).Entity;
        }

        public Usuario GetById(long id)
        {
            return _ctx.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            return _ctx.Usuarios.FirstOrDefault(x => x.Email == email);
        }

        public Usuario Update(Usuario usuario)
        {
            return _ctx.Usuarios.Update(usuario).Entity;
        }
    }
}
