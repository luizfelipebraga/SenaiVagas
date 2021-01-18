using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.Inputs
{
    public class AreaDeInteresseCandidatoInput
    {
        [Required(ErrorMessage = "A lista de áreas de interesse do candidato não pode estar vazia.")]
        public List<AreaInput> AreasInteresse { get; set; }
    }
}
