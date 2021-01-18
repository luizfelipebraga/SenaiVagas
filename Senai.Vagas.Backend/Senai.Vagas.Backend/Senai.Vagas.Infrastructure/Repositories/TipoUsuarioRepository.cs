using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public TipoUsuarioRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public TipoUsuario Create(TipoUsuario objeto)
        {
            return _ctx.TipoUsuarios.Add(objeto).Entity;
        }

        public TipoUsuario GetById(long id)
        {
            return _ctx.TipoUsuarios.FirstOrDefault(x => x.Id == id);
        }

        public TipoUsuario GetByDescricao(string descricao)
        {
            return _ctx.TipoUsuarios.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
