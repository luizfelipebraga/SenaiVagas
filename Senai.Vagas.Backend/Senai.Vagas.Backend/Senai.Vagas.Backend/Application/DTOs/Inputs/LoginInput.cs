using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//clsse do modelo do login
namespace Senai.Vagas.Backend.Application.DTOs.Inputs
{
    public class LoginInput
    {
        [Required(ErrorMessage = "Informe o e-mail do usuário!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuário!")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
