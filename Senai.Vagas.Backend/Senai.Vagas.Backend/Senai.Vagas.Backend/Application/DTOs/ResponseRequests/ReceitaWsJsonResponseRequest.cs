using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.ResponseRequests
{
    public class ReceitaWsJsonResponseRequest
    {
        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "data_situacao")]
        public string DataSituacao { get; set; }

        [JsonProperty(PropertyName = "complemento")]
        public string Complemento { get; set; }

        [JsonProperty(PropertyName = "tipo")]
        public string TipoEmpresa { get; set; }

        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "situacao")]
        public string Situacao { get; set; }

        [JsonProperty(PropertyName = "bairro")]
        public string Bairro { get; set; }

        [JsonProperty(PropertyName = "logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty(PropertyName = "numero")]
        public string Numero { get; set; }

        [JsonProperty(PropertyName = "cep")]
        public string CEP { get; set; }

        [JsonProperty(PropertyName = "municipio")]
        public string Municipio { get; set; }

        [JsonProperty(PropertyName = "uf")]
        public string Uf { get; set; }

        [JsonProperty(PropertyName = "fantasia")]
        public string Fantasia { get; set; }

        [JsonProperty(PropertyName = "porte")]
        public string Porte { get; set; }

        [JsonProperty(PropertyName = "abertura")]
        public string Abertura { get; set; }

        [JsonProperty(PropertyName = "natureza_juridica")]
        public string NaturezaJuridica { get; set; }

        [JsonProperty(PropertyName = "cnpj")]
        public string CNPJ { get; set; }

        [JsonProperty(PropertyName = "ultima_atualizacao")]
        public string UltimaAtualizacao { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "telefone")]
        public string Telefone { get; set; }

        [JsonProperty(PropertyName = "efr")]
        public string EFR { get; set; }

        [JsonProperty(PropertyName = "motivo_situacao")]
        public string MotivoSituacao { get; set; }

        [JsonProperty(PropertyName = "situacao_especial")]
        public string SituacaoEspecial { get; set; }

        [JsonProperty(PropertyName = "data_situacao_especial")]
        public string DataSituacaoEspecial { get; set; }

        [JsonProperty(PropertyName = "capital_social")]
        public string CapitalSocial { get; set; }

        [JsonProperty(PropertyName = "extra")]
        public object Extra { get; set; }

        [JsonProperty(PropertyName = "atividade_principal")]
        public List<AtividadePrincipal> AtividadePrincipal { get; set; }

        [JsonProperty(PropertyName = "atividades_secundarias")]
        public List<AtividadeSecundaria> AtividadeSecundaria { get; set; }

        [JsonProperty(PropertyName = "qsa")]
        public List<QSA> QSA { get; set; }

        [JsonProperty(PropertyName = "billing")]
        public Billing Billing { get; set; }
    }

    public class AtividadePrincipal
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }

    public class AtividadeSecundaria
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }
    }

    public class QSA
    {
        [JsonProperty(PropertyName = "qual")]
        public string Qual { get; set; }
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }
    }

    public class Billing
    {
        [JsonProperty(PropertyName = "free")]
        public bool Free { get; set; }
        [JsonProperty(PropertyName = "database")]
        public bool Database { get; set; }
    }
}

