using Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class TipoEmpresaRepository : ITipoEmpresaRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public TipoEmpresaRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public TipoEmpresa Create(TipoEmpresa objeto)
        {
            return _ctx.TipoEmpresas.Add(objeto).Entity;
        }

        public TipoEmpresa GetById(long id)
        {
            return _ctx.TipoEmpresas.FirstOrDefault(x => x.Id == id);
        }

        public TipoEmpresa GetByDescricao(string descricao)
        {
            return _ctx.TipoEmpresas.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
