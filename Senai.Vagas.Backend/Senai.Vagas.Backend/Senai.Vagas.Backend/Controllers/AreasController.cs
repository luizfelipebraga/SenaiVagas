using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;

namespace Senai.Vagas.Backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        public IAreaQueries _areaQueries { get; set; }

        public AreasController(IAreaQueries areaQueries)
        {
            _areaQueries = areaQueries;
        }

        /// <summary>
        /// Buscar areas de interesse
        /// </summary>
        /// <returns></returns>
        [HttpGet("Buscar-areas")]
        public IActionResult GetAllAreasInteresses()
        {
            try
            {
                List<AreaViewModel> areaInteresse = _areaQueries.GetAllAreaInteresse();
                return StatusCode(200, areaInteresse);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao retornar todas as áreas de interesses.");
            }
        }
    }
}
