using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate
{
    public interface IUsuarioCandidatoAlunoRepository : IRepository<UsuarioCandidatoAluno>
    {
        PerfilUsuarioCandidatoAluno CreatePerfilUsuarioCandidatoAluno(PerfilUsuarioCandidatoAluno perfilUsuarioCandidatoAluno);
        PerfilUsuarioCandidatoAluno GetPerfilUsuarioCandidatoAlunoByUsuarioCandidatoId(long usuarioCandidatoId);
        AreasInteresseCandidatoAluno CreateAreasInteresseCandidatoAluno(AreasInteresseCandidatoAluno perfilAreaUsuarioCandidatoAluno);
        List<AreasInteresseCandidatoAluno> CreateRangeAreasInteresseCandidatoAluno(List<AreasInteresseCandidatoAluno> perfisAreasUsuariosCandidatosAlunos);
        AreasInteresseCandidatoAluno GetAreasInteresseCandidatoAlunoByAreaIdAndCandidatoId(long areaId, long usuarioCandidatoAlunoId);
        AreasInteresseCandidatoAluno UpdateAreasInteresseCandidatoAluno(AreasInteresseCandidatoAluno perfilAreaUsuarioCandidatoAluno);
        PerfilUsuarioCandidatoAluno UpdatePerfilUsuarioCandidatoAluno(PerfilUsuarioCandidatoAluno perfilUsuarioCandidatoAluno);
        List<AreasInteresseCandidatoAluno> GetAllAreasInteresseByUsuarioCandidatoId(long usuarioCandidatoAlunoId);
        UsuarioCandidatoAluno GetByAlunoId(long alunoId);
        UsuarioCandidatoAluno GetByUsuarioId(long usuarioId);
    }
}
