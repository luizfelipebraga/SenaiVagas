using Microsoft.AspNetCore.Mvc;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienciasController : ControllerBase
    {
        public IExperienciaQueries _experienciaQueries { get; set; }

        public ExperienciasController(IExperienciaQueries experienciaQueries)
        {
            _experienciaQueries = experienciaQueries;
        }

        /// <summary>
        /// Buscar os níveis de experiencias
        /// </summary>
        /// <returns></returns>
        [HttpGet("Buscar-tipos-experiencia")]
        public IActionResult GetAllTiposExperiencias()
        {
            try
            {
                List<TipoExperienciaViewModel> tipoExperiencia = _experienciaQueries.GetAllTipoExperiencias();
                return StatusCode(200, tipoExperiencia);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao visualizar os tipos de experiencia!");
            }
        }
    }
}
