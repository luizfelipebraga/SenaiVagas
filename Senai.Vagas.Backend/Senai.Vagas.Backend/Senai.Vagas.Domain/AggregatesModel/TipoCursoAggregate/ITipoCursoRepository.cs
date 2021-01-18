using Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate
{
    public interface ITipoCursoRepository : IRepository<TipoCurso>
    {
        TipoCurso GetByDescricao(string descricao);
        List<TipoCurso> CreateRange(List<TipoCurso> tipoCursos);
    }
}
