using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate
{
    public interface IHistoricoStatusUsuarioRepository : IRepository<HistoricoStatusUsuario>
    {
        HistoricoStatusUsuario GetHistoricoAtualByUsuarioId(long usuarioId);
        void PutAlterarHistorico(HistoricoStatusUsuario historicoStatusUsuario);
    }
}
