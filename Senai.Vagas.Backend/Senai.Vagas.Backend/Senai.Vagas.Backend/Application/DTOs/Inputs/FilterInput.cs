using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.Inputs
{
    public class FilterInput
    {
        [Required(ErrorMessage = "O campo de filtro deve ser preenchido.")]
        public string Filter { get; set; }
    }
}
