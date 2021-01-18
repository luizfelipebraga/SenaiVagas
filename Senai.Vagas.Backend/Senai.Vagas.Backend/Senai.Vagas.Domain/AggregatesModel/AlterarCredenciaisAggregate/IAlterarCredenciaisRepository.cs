using Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate
{
    public interface IAlterarCredenciaisRepository : IRepository<AlterarCredenciais>
    {
        AlterarCredenciais GetAtualByToken(string token);
        AlterarCredenciais GetAtualByUsuarioId(long usuarioId);
        AlterarCredenciais UpdateAlterarCredenciais(AlterarCredenciais alterarCredenciais);
    }
}


