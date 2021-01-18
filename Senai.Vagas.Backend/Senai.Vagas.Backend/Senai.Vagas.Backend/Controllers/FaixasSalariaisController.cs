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
    public class FaixasSalariaisController : ControllerBase
    {
        public IFaixaSalarialQueries _faixaSalarialQueries { get; set; }

        public FaixasSalariaisController(IFaixaSalarialQueries faixaSalarialQueries)
        {
            _faixaSalarialQueries = faixaSalarialQueries;
        }

        /// <summary>
        /// Buscar as faixas salariais
        /// </summary>
        /// <returns></returns>
        [HttpGet("Buscar-faixas-salariais")]
        public IActionResult GetAllFaixasSalariais()
        {
            try
            {
                List<FaixaSalarialViewModel> faixaSalarial = _faixaSalarialQueries.GetAllFaixaSalariais();
                return StatusCode(200, faixaSalarial);
            }
            catch (Exception)
            {
                return StatusCode(500, "Houve um erro interno ao visualizar as faixas salariais!");
            }
        }
    }
}
