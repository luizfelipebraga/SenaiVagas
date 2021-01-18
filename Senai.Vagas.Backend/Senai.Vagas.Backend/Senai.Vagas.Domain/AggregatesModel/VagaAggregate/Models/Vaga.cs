using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.VagaAggregate
{
   public class Vaga : AbstractDomain
    {
        public string NomeVaga { get; set; }
        public string Cargo { get; set; }
        public string DescricaoVaga { get; set; }
        public DateTime DataEncerramento { get; set; }
        public long StatusVagaId { get; set; }
        public long TipoExperienciaId { get; set; }
        public long UsuarioEmpresaId { get; set; }
        public long MunicipioId { get; set; }
        public long FaixaSalarialId { get; set; }

        public Vaga(string nomeVaga, string cargo, string descricaoVaga, DateTime dataEncerramento, long statusVagaId, long tipoExperienciaId, long usuarioEmpresaId, long municipioId, long faixaSalarialId)
        {
            NomeVaga = nomeVaga;
            Cargo = cargo;
            DescricaoVaga = descricaoVaga;
            DataEncerramento = dataEncerramento;
            StatusVagaId = statusVagaId;
            TipoExperienciaId = tipoExperienciaId;
            UsuarioEmpresaId = usuarioEmpresaId;
            MunicipioId = municipioId;
            FaixaSalarialId = faixaSalarialId;
        }

        public void AlterarStatusVaga(long statusVagaId)
        {
            StatusVagaId = statusVagaId;
        }

        public void AlterarInformacoesVaga(string nomeVaga, string cargo, string descricaoVaga, DateTime dataEncerramento, long tipoExperienciaId, long municipioId, long faixaSalarialId)
        {
            NomeVaga = nomeVaga;
            Cargo = cargo;
            DescricaoVaga = descricaoVaga;
            DataEncerramento = dataEncerramento;
            TipoExperienciaId = tipoExperienciaId;
            MunicipioId = municipioId;
            FaixaSalarialId = faixaSalarialId;
        }
    }
}
