using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate.Models
{
    public class ValidacaoUsuarioCandidato : AbstractDomain
    {
       public string Token { get; set; }
	   public DateTime DataValida { get; set; }
	   public bool Ativo { get; set; }
       public long AlunoId { get; set; }

        public ValidacaoUsuarioCandidato(string token , long alunoId)
        {
            Token = token;
            AlunoId = alunoId;
            Ativo = true;
            DataValida = DateTime.Now.AddMinutes(30);
        }
        
       public void AlterarParaInativo()
        {
            Ativo = false;
        }
    }
}
