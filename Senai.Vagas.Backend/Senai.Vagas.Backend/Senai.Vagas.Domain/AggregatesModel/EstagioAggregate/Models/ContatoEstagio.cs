using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.EstagioAggregate.Models
{
    public class ContatoEstagio : AbstractDomain
    {
        public string TelefoneOuEmail { get; set; }
        public long EstagioId { get; set; }
        public ContatoEstagio(string telefoneOuEmail, long estagioId)
        {
            TelefoneOuEmail = telefoneOuEmail;
            EstagioId = estagioId;
        }
    }
}
