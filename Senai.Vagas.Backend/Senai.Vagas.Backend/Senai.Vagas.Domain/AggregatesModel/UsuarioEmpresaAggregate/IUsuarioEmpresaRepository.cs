using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate
{
    public interface IUsuarioEmpresaRepository : IRepository<UsuarioEmpresa>
    {
        UsuarioEmpresa GetByEmpresaId(long empresaId);
        UsuarioEmpresa GetByUsuarioId(long usuarioId);
    }
}
