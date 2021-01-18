using Senai.Vagas.Backend.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.Interfaces
{
    public interface IVagaQueries
    {
        VagaViewModel BuscarTodasInformacoesVagaPorId(long vagaId);
        List<VagaViewModel> GetAllVagasByUsuarioEmpresaId(long usuarioEmpresaId);
        List<VagaViewModel> GetAllVagas();
        List<VagaViewModel> BuscarVagasPorFiltro(string text, long usuarioCandidatoAlunoId);
        List<VagaViewModel> BuscarVagasPorPerfilAreaCandidato(long usuarioCandidatoAlunoId);
        List<StatusVagaViewModel> BuscarAllStatusVaga();
    }
}
