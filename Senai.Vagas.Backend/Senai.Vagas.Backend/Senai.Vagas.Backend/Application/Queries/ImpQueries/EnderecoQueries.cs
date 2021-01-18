using AutoMapper;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ImpQueries
{
    public class EnderecoQueries : IEnderecoQueries
    {
        public SenaiVagasContext _ctx { get; set; }
        public IMapper _mapper { get; set; }

        public EnderecoQueries(SenaiVagasContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public List<MunicipioViewModel> GetAllMunicipio()
        {
            var municipios = _mapper.Map<List<MunicipioViewModel>>(_ctx.Municipios.ToList());

            municipios.ForEach(x =>
            {
                x.UfSigla = _mapper.Map<UfSiglaViewModel>(_ctx.UfSiglas.FirstOrDefault(y => y.Id == x.UfSigla.Id));
            });

            return municipios;
        }
    }
}
