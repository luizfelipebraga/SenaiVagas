using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ViewModels
{
    public class EmpresaViewModel
    {
        public long Id { get; set; }
        public string CNPJ { get; set; }
        public string EmailUsuario { get; set; }
        public string Nome { get; set; }
        public EnderecoViewModel Endereco { get; set; }
        public TipoEmpresaViewModel TipoEmpresa { get; set; }
        public List<QSAViewModel> QSAs { get; set; }
        public AtividadeCnaeViewModel AtividadePrincipal { get; set; }
        public List<AtividadeCnaeViewModel> AtividadesSecundarias { get; set; }

        public void AcrescentarEmailUsuario(string emailUsuario)
        {
            EmailUsuario = emailUsuario;
        }
    }
}
