using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class VagaViewModel
    {
        public long Id { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeVaga { get; set; }
        public string Cargo { get; set; }
        public string DescricaoVaga { get; set; }
        public bool VagaAtiva { get; set; }
        public bool CandidatoRecebeuConvite { get; set; }
        public DateTime DataEncerramento { get; set; }
        public StatusVagaViewModel StatusVaga { get; set; }
        public FaixaSalarialViewModel FaixaSalarial { get; set; }
        public TipoExperienciaViewModel TipoExperiencia { get; set; }
        public long UsuarioEmpresaId { get; set; }
        public MunicipioViewModel Municipio { get; set; }
        public List<AreaVagaRecomendadaViewModel> AreaVagaRecomendadas { get; set; }
    }
}
