using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.Inputs
{
    public class SolicitacaoAlterarCredenciaisInput
    {
        [Required(ErrorMessage = "O campo do Email Atual deve estar preenchido.")]
        public string EmailAtual { get; set; }
        public string NovoEmail { get; set; }
    }
}
