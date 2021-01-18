using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.AlunoAggregate
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Aluno BuscarPorEmail(string email);
        Aluno BuscarPorEmailRMANome(string rma, string email, string nome);
        Aluno BuscarPorEmailouRMA(string RMAouEmail);
        List<Aluno> CreateRange(List<Aluno> alunos);
        Aluno UpdateAluno(Aluno aluno);
    }
    
}
