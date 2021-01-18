using Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate
{
    public interface ITipoEmpresaRepository : IRepository <TipoEmpresa> 
    {
        TipoEmpresa GetByDescricao(string descricao);
    }
}
