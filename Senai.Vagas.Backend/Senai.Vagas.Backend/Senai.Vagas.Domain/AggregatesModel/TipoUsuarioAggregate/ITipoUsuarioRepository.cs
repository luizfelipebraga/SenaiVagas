using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate
{
    public interface ITipoUsuarioRepository : IRepository<TipoUsuario>
    {
        TipoUsuario GetByDescricao(string descricao);
    }
}
