using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;

        public EnderecoRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public Endereco Create(Endereco objeto)
        {
            return _ctx.Enderecos.Add(objeto).Entity;
        }

        public Municipio CreateMunicipio(Municipio objeto)
        {
            return _ctx.Municipios.Add(objeto).Entity;
        }

        public UfSigla CreateUfSigla(UfSigla objeto)
        {
            return _ctx.UfSiglas.Add(objeto).Entity;
        }

        public Endereco GetById(long id)
        {
            return _ctx.Enderecos.FirstOrDefault(x => x.Id == id);
        }

        public Municipio GetMunicipioById(long id)
        {
            return _ctx.Municipios.FirstOrDefault(x => x.Id == id);
        }

        public UfSigla GetUfSiglaById(long id)
        {
            return _ctx.UfSiglas.FirstOrDefault(x => x.Id == id);
        }

        public Municipio GetMunicipioByDescricao(string descricao)
        {
            return _ctx.Municipios.FirstOrDefault(x => x.Descricao == descricao);
        }

        public UfSigla GetUfSiglaBySigla(string UfSigla)
        {
            return _ctx.UfSiglas.FirstOrDefault(x => x.UFSigla == UfSigla);
        }

        public List<Municipio> CreateRangeMunicipio(List<Municipio> municipios)
        {
            _ctx.Municipios.AddRange(municipios);

            return municipios;
        }

        public List<UfSigla> CreateRangeUfSigla(List<UfSigla> UfsSiglas)
        {
            _ctx.UfSiglas.AddRange(UfsSiglas);

            return UfsSiglas;
        }
    }
}
