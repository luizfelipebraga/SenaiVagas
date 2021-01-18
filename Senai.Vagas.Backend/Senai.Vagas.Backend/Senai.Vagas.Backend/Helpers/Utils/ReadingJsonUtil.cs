using Newtonsoft.Json;
using Senai.Vagas.Backend.Application.DTOs.ResponseRequests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Utils
{
    public class ReadingJsonUtil
    {
        public static CredenciaisEmailResponseRequest GetCredenciaisEmail()
        {
            // Declara a variavel do arquivo
            string arquivo;

            // Tenta instacia-lo
            try
            {
                arquivo = File.ReadAllText(@".\Helpers\FileData\credenciais_email.json");
            }
            catch
            {
                return null;
            }

            // Caso não ache o arquivo retorna nulo
            if (string.IsNullOrEmpty(arquivo))
            {
                return null;
            }

            // Deserializa as credenciais
            var credenciais = JsonConvert.DeserializeObject<CredenciaisEmailResponseRequest>(arquivo);

            // Retorna as credenciais
            return credenciais;
        }

        public static UrlFrontendResponseRequest GetUrlFrontend()
        {
            // Declara a variavel do arquivo
            string arquivo;

            // Tenta instacia-lo
            try
            {
                arquivo = File.ReadAllText(@".\Helpers\FileData\url_frontend.json");
            }
            catch
            {
                return null;
            }

            // Caso não ache o arquivo retorna nulo
            if (string.IsNullOrEmpty(arquivo))
            {
                return null;
            }

            // Deserializa as credenciais
            var url = JsonConvert.DeserializeObject<UrlFrontendResponseRequest>(arquivo);

            // Retorna as credenciais
            return url;
        }
    }
}
