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
    public class AreaQueries : IAreaQueries
    {
        public SenaiVagasContext _ctx;
        public IMapper _mapper;

        public AreaQueries(SenaiVagasContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public List<AreaViewModel> GetAllAreaInteresse()
        {
            var areaInteresseDb = _mapper.Map<List<AreaViewModel>>(_ctx.Areas.ToList());

            return areaInteresseDb;
        }
    }
}
