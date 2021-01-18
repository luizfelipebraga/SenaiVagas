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
    public class ExperienciaQueries : IExperienciaQueries
    {
        public SenaiVagasContext _ctx { get; set; }
        public IMapper _mapper { get; set; }

        public ExperienciaQueries(SenaiVagasContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public List<TipoExperienciaViewModel> GetAllTipoExperiencias()
        {
            var tiposExperiencias = _mapper.Map<List<TipoExperienciaViewModel>>(_ctx.TipoExperiencias.ToList());

            return tiposExperiencias;
        }
    }
}
