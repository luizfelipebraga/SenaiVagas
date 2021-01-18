using AutoMapper;
using Senai.Vagas.Backend.Application.Queries.ViewModels;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.AreaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.EstagioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusEstagioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Vagas.Backend.Helpers.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        // Aqui estará todas as conversões de Domain Para ViewModel ou vice versa
        public AutoMapperProfiles()
        {
            // Domain para ViewModel

            CreateMap<Usuario, UsuarioViewModel>()
                .ForPath(s => s.TipoUsuario.Id, d => d.MapFrom(m => m.TipoUsuarioId));

            CreateMap<UsuarioCandidatoAluno, UsuarioCandidatoAlunoViewModel>()
                .ForPath(s => s.Usuario.Id, d => d.MapFrom(m => m.UsuarioId))
                .ForPath(s => s.Aluno.Id, d => d.MapFrom(m => m.AlunoId));

            CreateMap<UsuarioEmpresa, UsuarioEmpresaViewModel>()
                .ForPath(s => s.Usuario.Id, d => d.MapFrom(m => m.UsuarioId))
                .ForPath(s => s.Empresa.Id, d => d.MapFrom(m => m.EmpresaId));

            CreateMap<Empresa, EmpresaViewModel>()
                .ForPath(s => s.TipoEmpresa.Id, d => d.MapFrom(m => m.TipoEmpresaId))
                .ForPath(s => s.Endereco.Id, d => d.MapFrom(m => m.EnderecoId));

            CreateMap<AtividadeCnae, AtividadeCnaeViewModel>()
                .ForPath(s => s.TipoCnae.Id, d => d.MapFrom(m => m.TipoCnaeId))
                .ForPath(s => s.TipoAtividadeCnae.Id, d => d.MapFrom(m => m.TipoAtividadeCnaeId));

            CreateMap<QSA, QSAViewModel>();

            CreateMap<Endereco, EnderecoViewModel>()
                .ForPath(s => s.Municipio.Id, d => d.MapFrom(m => m.MunicipioId));

            CreateMap<Municipio, MunicipioViewModel>()
                .ForPath(s => s.UfSigla.Id, d => d.MapFrom(m => m.UfSiglaId));

            CreateMap<UfSigla, UfSiglaViewModel>();

            CreateMap<UsuarioAdministrador, UsuarioAdministradorViewModel>()
                .ForPath(s => s.Usuario.Id, d => d.MapFrom(m => m.UsuarioId));

            CreateMap<Vaga, VagaViewModel>()
                .ForPath(s => s.TipoExperiencia.Id, d => d.MapFrom(m => m.TipoExperienciaId))
                .ForPath(s => s.StatusVaga.Id, d => d.MapFrom(m => m.StatusVagaId))
                .ForPath(s => s.Municipio.Id, d => d.MapFrom(m => m.MunicipioId))
                .ForPath(s => s.FaixaSalarial.Id, d => d.MapFrom(m => m.FaixaSalarialId));

            CreateMap<AreaVagaRecomendada, AreaVagaRecomendadaViewModel>()
                .ForPath(s => s.Area.Id, d => d.MapFrom(m => m.AreaId));

            CreateMap<Inscricao, InscricaoViewModel>()
                .ForPath(s => s.UsuarioCandidatoAluno.Id, d => d.MapFrom(m => m.UsuarioCandidatoAlunoId));

            CreateMap<ConviteEntrevista, ConviteEntrevistaViewModel>()
                .ForPath(s => s.Endereco.Id, d => d.MapFrom(m => m.EnderecoId));

            CreateMap<AreasInteresseCandidatoAluno, AreasInteresseCandidatoAlunoViewModel>()
                .ForPath(s => s.Area.Id, d => d.MapFrom(m => m.AreaId));

            CreateMap<PerfilUsuarioCandidatoAluno, PerfilUsuarioCandidatoAlunoViewModel>();

            CreateMap<Estagio, EstagioViewModel>()
                .ForPath(s => s.Endereco.Id, d => d.MapFrom(m => m.EnderecoId))
                .ForPath(s => s.PessoaResponsavel.Id, d => d.MapFrom(m => m.PessoaResponsavelId))
                .ForPath(s => s.TermoOuEgressoAluno.Id, d => d.MapFrom(m => m.TermoOuEgressoAlunoId))
                .ForPath(s => s.RequerimentoMatricula.Id, d => d.MapFrom(m => m.RequerimentoMatriculaId))
                .ForPath(s => s.UsuarioEmpresa.Id, d => d.MapFrom(m => m.UsuarioEmpresaId))
                .ForPath(s => s.Aluno.Id, d => d.MapFrom(m => m.AlunoId));

            CreateMap<ContatoEstagio, ContatoEstagioViewModel>()
                .ForPath(s => s.Estagio.Id, d => d.MapFrom(m => m.EstagioId));

            CreateMap<PessoaResponsavel, PessoaResponsavelViewModel>();

            CreateMap<HistoricoStatusEstagio, HistoricoStatusEstagioViewModel>()
                .ForPath(s => s.StatusEstagio.Id, d => d.MapFrom(m => m.StatusEstagioId))
                .ForPath(s => s.Estagio.Id, d => d.MapFrom(m => m.EstagioId));

            CreateMap<HistoricoStatusUsuario, HistoricoStatusUsuarioViewModel>()
                .ForPath(s => s.Usuario.Id, d => d.MapFrom(m => m.UsuarioId))
                .ForPath(s => s.StatusUsuario.Id, d => d.MapFrom(m => m.StatusUsuarioId));

            CreateMap<Aluno, AlunoViewModel>()
                .ForPath(s => s.TipoCurso.Id, d => d.MapFrom(m => m.TipoCursoId))
                .ForPath(s => s.TermoOuEgressoAluno.Id, d => d.MapFrom(m => m.TermoOuEgressoAlunoId));


            CreateMap<TipoUsuario, TipoUsuarioViewModel>();
            CreateMap<TipoExperiencia, TipoExperienciaViewModel>();
            CreateMap<TipoEmpresa, TipoEmpresaViewModel>();
            CreateMap<TipoCurso, TipoCursoViewModel>();
            CreateMap<TipoCnae, TipoCnaeViewModel>();
            CreateMap<FaixaSalarial, FaixaSalarialViewModel>();
            CreateMap<TipoAtividadeCnae, TipoAtividadeCnaeViewModel>();
            CreateMap<StatusVaga, StatusVagaViewModel>();
            CreateMap<StatusUsuario, StatusUsuarioViewModel>();
            CreateMap<StatusEstagio, StatusEstagioViewModel>();
            CreateMap<RequerimentoMatricula, RequerimentoMatriculaViewModel>();
            CreateMap<TermoOuEgressoAluno, TermoOuEgressoAlunoViewModel>();
            CreateMap<Area, AreaViewModel>();
        }
    }
}
