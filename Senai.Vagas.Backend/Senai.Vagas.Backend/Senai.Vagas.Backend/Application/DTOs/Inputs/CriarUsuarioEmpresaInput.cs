using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.Inputs
{
    [DataContract]
    public class CriarUsuarioEmpresaInput
    {
        [Required]
        [DataMember]
        public string Nome { get; set; }
        [Required]
        [DataMember]
        public string Email { get; set; }
        [Required]
        [DataMember]
        public string Cnpj { get; set; }
        [Required]
        [DataMember]
        public string Senha { get; set; }
    }
}
