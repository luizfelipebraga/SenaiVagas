using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.Inputs
{
    public class CriarConviteInput
    {

        [Required]
        public string Rua { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Numero { get; set; }
        public string infosComplementares { get; set; }
        [Required]
        public MunicipioInput Municipio { get; set; }
        [Required]
        public DateTime DataHoraEntrevista { get; set; }
        
    }
}
