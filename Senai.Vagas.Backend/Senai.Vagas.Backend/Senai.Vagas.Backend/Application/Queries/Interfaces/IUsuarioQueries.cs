using Senai.Vagas.Backend.Application.Queries.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.Interfaces
{
    public interface IUsuarioQueries
    {
        List<AreasInteresseCandidatoAlunoViewModel> GetAllAreasInteresseCandidatoByCandidatoId(long usuarioCandidatoAlunoId);
        AlunoViewModel GetInformacoesPerfilAlunoByAlunoId(long alunoId);
        List<AlunoViewModel> GetAllAlunos();
    }
}
