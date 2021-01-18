using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate
{
    public interface IStatusUsuarioRepository : IRepository<StatusUsuario>
    {
        StatusUsuario GetByDescricao(string descricao);
    }
}
