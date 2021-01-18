using Senai.Vagas.Domain.AggregatesModel.EstagioAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EstagioAggregate
{
    public interface IEstagioRepository : IRepository<Estagio>
    {
        ContatoEstagio CreateContatoEstagio(ContatoEstagio contatoEstagio);
        ContatoEstagio GetContatoEstagioById(long id);

        PessoaResponsavel CreatePessoaResponsavel(PessoaResponsavel pessoaResponsavel);
        PessoaResponsavel GetPessoaResponsavelById(long id);
    }
}
