using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.ResponseViewModels
{
    public class ConviteEntrevistaViewModel
    {
        public long Id { get; set; }
        public string NomeEmpresa { get; set; }
        public DateTime DataHoraEntrevista { get; set; }
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Municipio { get; set; }
        public string UfSigla { get; set; }
        public string InfosComplementares{ get; set; }

        public ConviteEntrevistaViewModel(long id, string nomeEmpresa, DateTime dataHoraEntrevista, string rua, string bairro, string numero, string municipio, string ufSigla, string infosComplementares)
        {
            Id = id;
            NomeEmpresa = nomeEmpresa;
            DataHoraEntrevista = dataHoraEntrevista;
            Rua = rua;
            Bairro = bairro;
            Numero = numero;
            Municipio = municipio;
            UfSigla = ufSigla;
            InfosComplementares = infosComplementares;
        }
    }
}
