using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        public SenaiVagasContext _ctx;
        public IUnitOfWork UnitOfWork => _ctx;

        public AlunoRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }
        public Aluno BuscarPorEmail(string email)
        {
            return _ctx.Alunos.FirstOrDefault(x => x.Email == email);
        }

        public Aluno Create(Aluno objeto)
        {
            return _ctx.Alunos.Add(objeto).Entity;
        }

        public Aluno GetById(long id)
        {
            return _ctx.Alunos.FirstOrDefault(x => x.Id == id);
        }

        public List<Aluno> CreateRange(List<Aluno> alunos)
        {
            _ctx.Alunos.AddRange(alunos);

            return alunos;
        }

        public Aluno BuscarPorEmailRMANome(string rma, string email, string nome)
        {
            return _ctx.Alunos.FirstOrDefault(x => x.RMA == rma || x.Email == email || x.NomeCompleto == nome);
        }

        public Aluno BuscarPorEmailouRMA(string RMAouEmail)
        {
            return _ctx.Alunos.FirstOrDefault(x => x.RMA == RMAouEmail || x.Email == RMAouEmail);
        }

        public Aluno UpdateAluno(Aluno aluno)
        {
            return _ctx.Alunos.Update(aluno).Entity;
        }
    }
}
