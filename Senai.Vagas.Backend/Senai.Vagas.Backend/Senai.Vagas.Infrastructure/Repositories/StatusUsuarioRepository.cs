using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class StatusUsuarioRepository : IStatusUsuarioRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public StatusUsuarioRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public StatusUsuario Create(StatusUsuario objeto)
        {
            return _ctx.StatusUsuarios.Add(objeto).Entity;
        }

        public StatusUsuario GetById(long id)
        {
            return _ctx.StatusUsuarios.FirstOrDefault(x => x.Id == id);
        }

        public StatusUsuario GetByDescricao(string descricao)
        {
            return _ctx.StatusUsuarios.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
