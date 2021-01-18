using Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class TermoOuEgressoAlunoRepository : ITermoOuEgressoAlunoRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public TermoOuEgressoAlunoRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }
        public TermoOuEgressoAluno Create(TermoOuEgressoAluno objeto)
        {
            return _ctx.TermoOuEgressoAlunos.Add(objeto).Entity;
        }

        public TermoOuEgressoAluno GetById(long id)
        {
            return _ctx.TermoOuEgressoAlunos.FirstOrDefault(x => x.Id == id);
        }

        public TermoOuEgressoAluno GetByDescricao(string descricao)
        {
            return _ctx.TermoOuEgressoAlunos.FirstOrDefault(x => x.Descricao == descricao);
        }
    }
}
