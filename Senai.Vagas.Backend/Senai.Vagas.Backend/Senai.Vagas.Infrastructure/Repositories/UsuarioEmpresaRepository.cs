using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class UsuarioEmpresaRepository : IUsuarioEmpresaRepository
    {
        public SenaiVagasContext _ctx { get; set; }

        public IUnitOfWork UnitOfWork => _ctx;

        public UsuarioEmpresaRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public UsuarioEmpresa Create(UsuarioEmpresa objeto)
        {
            return _ctx.UsuarioEmpresas.Add(objeto).Entity;
        }

        public UsuarioEmpresa GetById(long id)
        {
            return _ctx.UsuarioEmpresas.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioEmpresa GetByEmpresaId(long empresaId)
        {
            return _ctx.UsuarioEmpresas.FirstOrDefault(x => x.EmpresaId == empresaId);
        }

        public UsuarioEmpresa GetByUsuarioId(long usuarioId)
        {
            return _ctx.UsuarioEmpresas.FirstOrDefault(x => x.UsuarioId == usuarioId);
        }
    }
}
