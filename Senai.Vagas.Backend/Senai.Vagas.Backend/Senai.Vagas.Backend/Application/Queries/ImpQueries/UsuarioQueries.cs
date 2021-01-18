using AutoMapper;
using Senai.Vagas.Backend.Application.Queries.Interfaces;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Backend.Helpers.DefaultValues;
using Senai.Vagas.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Application.Queries.ImpQueries
{
    public class UsuarioQueries : IUsuarioQueries
    {
        public SenaiVagasContext _ctx;
        public IMapper _mapper;

        public UsuarioQueries(SenaiVagasContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public List<AlunoViewModel> GetAllAlunos()
        {
            // Busca statusUsuario Excluido na DB
            var statusUsuarioExcluidoDb = _ctx.StatusUsuarios.FirstOrDefault(x => x.Descricao == StatusUsuarioDefaultValuesAcess.GetValue(StatusUsuarioDefaultValues.ContaExcluida));

            // Busca todos os históricos com o StatusUsuarioId diferente de excluido
            var historicoStatusDb = _ctx.HistoricoStatusUsuarios.Where(x => x.StatusUsuarioId != statusUsuarioExcluidoDb.Id);

            // Busca todos os alunos que não estejam com contas excluídas
            var candidatosDb = _ctx.UsuarioCandidatoAlunos.Where(x => historicoStatusDb.Any(y => y.UsuarioId == x.UsuarioId));

            // Busca todos os alunos cadastrados na plataforma
            var alunos = _mapper.Map<List<AlunoViewModel>>(_ctx.Alunos.Where(x => candidatosDb.Any(y => y.AlunoId == x.Id)));

            // Itera entre todos os alunos para preencher todos os objetos
            alunos.ForEach(x =>
            {
                // Preenche TermoOuEgresso do aluno
                x.TermoOuEgressoAluno = _mapper.Map<TermoOuEgressoAlunoViewModel>(_ctx.TermoOuEgressoAlunos.FirstOrDefault(y => y.Id == x.TermoOuEgressoAluno.Id));

                // Preenche o tipoCurso do aluno
                x.TipoCurso = _mapper.Map<TipoCursoViewModel>(_ctx.TipoCursos.FirstOrDefault(y => y.Id == x.TipoCurso.Id));

                // Busca vinculo entre usuario X candidato no BD
                var usuarioCandidatoDb = _ctx.UsuarioCandidatoAlunos.FirstOrDefault(y => y.AlunoId == x.Id);

                // Caso existir, acrescenta o Perfil do Candidato (linkExterno + SobreOCandidato)
                if (usuarioCandidatoDb != null)
                    x.PerfilCandidato = _mapper.Map<PerfilUsuarioCandidatoAlunoViewModel>(_ctx.PerfilUsuarioCandidatoAlunos.FirstOrDefault(y => y.UsuarioCandidatoAlunoId == usuarioCandidatoDb.Id));
            });

            return alunos;
        }

        public List<AreasInteresseCandidatoAlunoViewModel> GetAllAreasInteresseCandidatoByCandidatoId(long usuarioCandidatoAlunoId)
        {
            // Busca todas as areas de interesse do candidato
            var perfisAreas = _mapper.Map<List<AreasInteresseCandidatoAlunoViewModel>>(_ctx.AreasInteresseCandidatoAlunos.Where(x => x.UsuarioCandidatoAlunoId == usuarioCandidatoAlunoId && x.Ativo));

            // Preenche o objeto Area, do perfilArea
            perfisAreas.ForEach(x =>
            {
                x.Area = _mapper.Map<AreaViewModel>(_ctx.Areas.FirstOrDefault(y => y.Id == x.Area.Id));
            });

            return perfisAreas;
        }

        public AlunoViewModel GetInformacoesPerfilAlunoByAlunoId(long alunoId)
        {
            // Busca aluno por Id
            var aluno = _mapper.Map<AlunoViewModel>(_ctx.Alunos.FirstOrDefault(x => x.Id == alunoId));

            // Caso não encontre
            if (aluno == null)
                return null;

            // Preenche TermoOuEgresso do aluno
            aluno.TermoOuEgressoAluno = _mapper.Map<TermoOuEgressoAlunoViewModel>(_ctx.TermoOuEgressoAlunos.FirstOrDefault(x => x.Id == aluno.TermoOuEgressoAluno.Id));

            // Preenche o tipoCurso do aluno
            aluno.TipoCurso = _mapper.Map<TipoCursoViewModel>(_ctx.TipoCursos.FirstOrDefault(x => x.Id == aluno.TipoCurso.Id));

            // Busca vinculo entre usuario X candidato no BD
            var usuarioCandidatoDb = _ctx.UsuarioCandidatoAlunos.FirstOrDefault(x => x.AlunoId == alunoId);

            // Caso existir, acrescenta o Perfil do Candidato (linkExterno + SobreOCandidato)
            if (usuarioCandidatoDb != null)
                aluno.PerfilCandidato = _mapper.Map<PerfilUsuarioCandidatoAlunoViewModel>(_ctx.PerfilUsuarioCandidatoAlunos.FirstOrDefault(x => x.UsuarioCandidatoAlunoId == usuarioCandidatoDb.Id));
            else
                aluno.PerfilCandidato = new PerfilUsuarioCandidatoAlunoViewModel();
            
            return aluno;
        }
    }
}
