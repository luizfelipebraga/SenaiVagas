using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.DTOs.ResponseRequests
{
    public class UrlFrontendResponseRequest
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        public UrlFrontendResponseRequest()
        {

        }

        public UrlFrontendResponseRequest(string url)
        {
            Url = url;
        }
    }
}
