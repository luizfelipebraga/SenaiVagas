using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        Empresa GetByCNPJ(string cnpj);
        QSA CreateQSA(QSA qsa);
        AtividadeCnae CreateAtividadeCnae(AtividadeCnae atividadeCnae);
        TipoCnae CreateTipoCnae(TipoCnae tipoCnae);
        TipoCnae GetTipoCnaeById(long id);
        TipoCnae GetTipoCnaeByCodigo(string codigo);
        AtividadeCnae GetAtividadeCnaeById(long id);
        QSA GetQSAById(long id);
    }
}
