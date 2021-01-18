using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class HistoricoStatusUsuarioRepository : IHistoricoStatusUsuarioRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public HistoricoStatusUsuarioRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }
        public HistoricoStatusUsuario Create(HistoricoStatusUsuario objeto)
        {
            return _ctx.HistoricoStatusUsuarios.Add(objeto).Entity;
        }

        public HistoricoStatusUsuario GetById(long id)
        {
            return _ctx.HistoricoStatusUsuarios.FirstOrDefault(x => x.Id == id);
        }

        public HistoricoStatusUsuario GetHistoricoAtualByUsuarioId(long usuarioId)
        {
            return _ctx.HistoricoStatusUsuarios.FirstOrDefault(x => x.UsuarioId == usuarioId && x.Atual);
        }

        public void PutAlterarHistorico(HistoricoStatusUsuario historicoStatusUsuario)
        {
           _ctx.HistoricoStatusUsuarios.Update(historicoStatusUsuario);
        }
    }
}
