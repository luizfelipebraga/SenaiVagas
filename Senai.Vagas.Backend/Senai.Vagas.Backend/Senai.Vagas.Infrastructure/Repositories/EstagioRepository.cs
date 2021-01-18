using Senai.Vagas.Domain.AggregatesModel.EstagioAggregate;
using Senai.Vagas.Domain.AggregatesModel.EstagioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class EstagioRepository : IEstagioRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;
        public EstagioRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public Estagio Create(Estagio objeto)
        {
            return _ctx.Estagios.Add(objeto).Entity;
        }

        public ContatoEstagio CreateContatoEstagio(ContatoEstagio contatoEstagio)
        {
            return _ctx.ContatoEstagios.Add(contatoEstagio).Entity;
        }

        public PessoaResponsavel CreatePessoaResponsavel(PessoaResponsavel pessoaResponsavel)
        {
            return _ctx.PessoaResponsaveis.Add(pessoaResponsavel).Entity;
        }

        public Estagio GetById(long id)
        {
            return _ctx.Estagios.FirstOrDefault(x => x.Id == id);
        }

        public ContatoEstagio GetContatoEstagioById(long id)
        {
            return _ctx.ContatoEstagios.FirstOrDefault(x => x.Id == id);
        }

        public PessoaResponsavel GetPessoaResponsavelById(long id)
        {
            return _ctx.PessoaResponsaveis.FirstOrDefault(x => x.Id == id);
        }
    }
}
