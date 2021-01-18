using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.ResponseRequests
{
    public class CredenciaisEmailResponseRequest
    {
        [JsonProperty(PropertyName = "emailRemetente")]
        public string EmailRemetente { get; private set; }

        [JsonProperty(PropertyName = "senha")]
        public string Senha { get; private set; }

        public CredenciaisEmailResponseRequest(string emailRemetente, string senha)
        {
            EmailRemetente = emailRemetente;
            Senha = senha;
        }

        public void AlterarCredenciais(string EmailRemetente, string Senha)
        {
            this.EmailRemetente = EmailRemetente;
            this.Senha = Senha;
        }
    }
}
