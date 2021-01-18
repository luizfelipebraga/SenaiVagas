using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.Inputs
{
    public class CriarVagaInput
    {
        [Required]
        public long UsuarioId { get; set; }
        [Required]
        public string NomeVaga { get; set; }
        [Required]
        public string Cargo { get; set; }
        [Required]
        public string DescricaoVaga { get; set; }
        [Required]
        public DateTime DataEncerramento { get; set; }
        [Required]
        public MunicipioInput Municipio { get; set; }
        [Required]
        public FaixaSalarialInput FaixaSalarial { get; set; }
        [Required]
        public TipoExperienciaInput TipoExperiencia { get; set; }
        [Required(ErrorMessage = "A lista de áreas não pode estar nula ou vazia.")]
        public List<AreaInput> AreasRecomendadas { get; set; }
    }

    public class MunicipioInput
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public UfSiglaInput UfSigla { get; set; }
    }

    public class UfSiglaInput
    {
        public long Id { get; set; }
        public string UFEstado { get; set; }
        public string UFSigla { get; set; }
    }

    public class AreaInput
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
    }

    public class FaixaSalarialInput
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
    }

    public class TipoExperienciaInput
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
    }
}
