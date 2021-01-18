using Senai.Vagas.Backend.Application.DTOs.ResponseRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.Services
{
    public class ApiRequestService
    {
        private HttpClient _client { get; set; }

        public ApiRequestService()
        {
            _client = new HttpClient();
        }

        public async Task<ReceitaWsJsonResponseRequest> ReceitaWS(string cnpj)
        {
            string uri = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";

            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(uri);

                ReceitaWsJsonResponseRequest receita = null;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Ocorreu algum erro na busca da Empresa.");
                }

                receita = await response.Content.ReadAsAsync<ReceitaWsJsonResponseRequest>();

                return receita;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ViaCepJsonResponseRequest> ViaCEP(string cep)
        {
            string uri = $"https://viacep.com.br/ws/{cep}/json/";

            HttpResponseMessage response = null;
            try
            {
                response = await _client.GetAsync(uri);

                ViaCepJsonResponseRequest viaCep = null;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Ocorreu algum erro na busca do CEP.");
                }

                viaCep = await response.Content.ReadAsAsync<ViaCepJsonResponseRequest>();

                return viaCep;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
