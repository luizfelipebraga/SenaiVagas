using Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate;
using Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class AlterarCredenciaisRepository : IAlterarCredenciaisRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public AlterarCredenciaisRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }     
        
        public AlterarCredenciais Create(AlterarCredenciais objeto)
        {
            return _ctx.AlterarCredenciais.Add(objeto).Entity;
        }

        public AlterarCredenciais GetById(long id)
        {
            return _ctx.AlterarCredenciais.FirstOrDefault(x => x.Id == id);
        }

        public AlterarCredenciais GetAtualByToken(string token)
        {
            return _ctx.AlterarCredenciais.FirstOrDefault(x => x.Token == token && x.Ativo);
        }

        public AlterarCredenciais GetAtualByUsuarioId(long usuarioId)
        {
            return _ctx.AlterarCredenciais.FirstOrDefault(x => x.UsuarioId == usuarioId && x.Ativo);
        }

        public AlterarCredenciais UpdateAlterarCredenciais(AlterarCredenciais alterarCredenciais)
        {
            return _ctx.AlterarCredenciais.Update(alterarCredenciais).Entity;
        }
    }
}
