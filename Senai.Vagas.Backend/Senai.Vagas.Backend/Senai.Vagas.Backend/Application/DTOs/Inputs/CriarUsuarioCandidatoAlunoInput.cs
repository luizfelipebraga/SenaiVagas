using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.Inputs
{
    public class CriarUsuarioCandidatoAlunoInput
    {
        [Required]
        public string Senha { get; set; }
    }
}
