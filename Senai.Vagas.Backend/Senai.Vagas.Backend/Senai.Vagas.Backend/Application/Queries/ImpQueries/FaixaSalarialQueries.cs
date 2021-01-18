using AutoMapper;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ImpQueries
{
    public class FaixaSalarialQueries : IFaixaSalarialQueries
    {
        public SenaiVagasContext _ctx { get; set; }
        public IMapper _mapper { get; set; }

        public FaixaSalarialQueries(SenaiVagasContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public List<FaixaSalarialViewModel> GetAllFaixaSalariais()
        {
            var faixasSalariais = _mapper.Map<List<FaixaSalarialViewModel>>(_ctx.FaixaSalariais.ToList());

            return faixasSalariais;
        }
    }
}
