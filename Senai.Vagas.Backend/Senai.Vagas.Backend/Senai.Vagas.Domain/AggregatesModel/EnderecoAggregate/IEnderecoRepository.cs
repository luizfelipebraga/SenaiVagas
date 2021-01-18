using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Municipio CreateMunicipio(Municipio objeto);
        List<Municipio> CreateRangeMunicipio(List<Municipio> municipios);
        UfSigla CreateUfSigla(UfSigla objeto);
        List<UfSigla> CreateRangeUfSigla(List<UfSigla> UfsSiglas);
        Municipio GetMunicipioById(long id);
        Municipio GetMunicipioByDescricao(string descricao);
        UfSigla GetUfSiglaById(long id);
        UfSigla GetUfSiglaBySigla(string UfSigla);

    }
}