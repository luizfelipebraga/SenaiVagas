using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.Inputs
{
    public class ConfirmarAlterarCredenciaisInput
    {
        [Required(ErrorMessage = "Sua senha não pode estar vazia.")]
        public string NovaSenha { get; set; }
    }
}
