using Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate
{
    public interface IRequerimentoMatriculaRepository : IRepository<RequerimentoMatricula>
    {
        RequerimentoMatricula GetByDescricao(string descricao);
    }
}
