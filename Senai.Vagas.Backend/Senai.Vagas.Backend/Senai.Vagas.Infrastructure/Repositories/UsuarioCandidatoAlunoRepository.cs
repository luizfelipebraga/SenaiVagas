using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Senai.Vagas.Infrastructure.Repositories
{
    public class UsuarioCandidatoAlunoRepository : IUsuarioCandidatoAlunoRepository
    {
        public SenaiVagasContext _ctx { get; set; }
        public IUnitOfWork UnitOfWork => _ctx;
        public UsuarioCandidatoAlunoRepository(SenaiVagasContext ctx)
        {
            _ctx = ctx;
        }

        public PerfilUsuarioCandidatoAluno CreatePerfilUsuarioCandidatoAluno(PerfilUsuarioCandidatoAluno perfilUsuarioCandidatoAluno)
        {
            return _ctx.PerfilUsuarioCandidatoAlunos.Add(perfilUsuarioCandidatoAluno).Entity;
        }

        public PerfilUsuarioCandidatoAluno GetPerfilUsuarioCandidatoAlunoByUsuarioCandidatoId(long usuarioCandidatoId)
        {
            return _ctx.PerfilUsuarioCandidatoAlunos.FirstOrDefault(x => x.UsuarioCandidatoAlunoId == usuarioCandidatoId);
        }

        public UsuarioCandidatoAluno Create(UsuarioCandidatoAluno objeto)
        {
            return _ctx.UsuarioCandidatoAlunos.Add(objeto).Entity;
        }

        public UsuarioCandidatoAluno GetById(long id)
        {
            return _ctx.UsuarioCandidatoAlunos.FirstOrDefault(x => x.Id == id);
        }

        public AreasInteresseCandidatoAluno CreateAreasInteresseCandidatoAluno(AreasInteresseCandidatoAluno perfilAreaUsuarioCandidatoAluno)
        {
            return _ctx.AreasInteresseCandidatoAlunos.Add(perfilAreaUsuarioCandidatoAluno).Entity;
        }

        public List<AreasInteresseCandidatoAluno> GetAllAreasInteresseByUsuarioCandidatoId(long usuarioCandidatoAlunoId)
        {
            return _ctx.AreasInteresseCandidatoAlunos.Where(x => x.UsuarioCandidatoAlunoId == usuarioCandidatoAlunoId && x.Ativo).ToList();
        }

        public UsuarioCandidatoAluno GetByAlunoId(long alunoId)
        {
            return _ctx.UsuarioCandidatoAlunos.FirstOrDefault(x => x.AlunoId == alunoId);
        }

        public UsuarioCandidatoAluno GetByUsuarioId(long usuarioId)
        {
            return _ctx.UsuarioCandidatoAlunos.FirstOrDefault(x => x.UsuarioId == usuarioId);
        }

        public AreasInteresseCandidatoAluno GetAreasInteresseCandidatoAlunoByAreaIdAndCandidatoId(long areaId, long usuarioCandidatoAlunoId)
        {
            return _ctx.AreasInteresseCandidatoAlunos.FirstOrDefault(x => x.UsuarioCandidatoAlunoId == usuarioCandidatoAlunoId && x.AreaId == areaId && x.Ativo);
        }

        public AreasInteresseCandidatoAluno UpdateAreasInteresseCandidatoAluno(AreasInteresseCandidatoAluno perfilAreaUsuarioCandidatoAluno)
        {
            return _ctx.AreasInteresseCandidatoAlunos.Update(perfilAreaUsuarioCandidatoAluno).Entity;
        }

        public List<AreasInteresseCandidatoAluno> CreateRangeAreasInteresseCandidatoAluno(List<AreasInteresseCandidatoAluno> perfisAreasUsuariosCandidatosAlunos)
        {
            _ctx.AreasInteresseCandidatoAlunos.AddRange(perfisAreasUsuariosCandidatosAlunos);

            return perfisAreasUsuariosCandidatosAlunos;
        }

        public PerfilUsuarioCandidatoAluno UpdatePerfilUsuarioCandidatoAluno(PerfilUsuarioCandidatoAluno perfilUsuarioCandidatoAluno)
        {
            return _ctx.PerfilUsuarioCandidatoAlunos.Update(perfilUsuarioCandidatoAluno).Entity;
        }
    }
}
