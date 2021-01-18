using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate;
using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public EmpresaRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public Empresa Create(Empresa objeto)
        {
            return _ctx.Empresas.Add(objeto).Entity;
        }

        public AtividadeCnae CreateAtividadeCnae(AtividadeCnae atividadeCnae)
        {
            return _ctx.AtividadeCnaes.Add(atividadeCnae).Entity;
        }

        public QSA CreateQSA(QSA qsa)
        {
            return _ctx.QSAs.Add(qsa).Entity;
        }

        public TipoCnae CreateTipoCnae(TipoCnae tipoCnae)
        {
            return _ctx.TipoCnaes.Add(tipoCnae).Entity;
        }

        public AtividadeCnae GetAtividadeCnaeById(long id)
        {
            return _ctx.AtividadeCnaes.FirstOrDefault(x => x.Id == id);
        }

        public Empresa GetById(long id)
        {
            return _ctx.Empresas.FirstOrDefault(x => x.Id == id);
        }

        public QSA GetQSAById(long id)
        {
            return _ctx.QSAs.FirstOrDefault(x => x.Id == id);
        }

        public TipoCnae GetTipoCnaeById(long id)
        {
            return _ctx.TipoCnaes.FirstOrDefault(x => x.Id == id);
        }

        public Empresa GetByCNPJ(string cnpj)
        {
            return _ctx.Empresas.FirstOrDefault(x => x.CNPJ == cnpj);
        }

        public TipoCnae GetTipoCnaeByCodigo(string codigo)
        {
            return _ctx.TipoCnaes.FirstOrDefault(x => x.Codigo == codigo);
        }
    }
}
