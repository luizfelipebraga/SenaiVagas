using Microsoft.EntityFrameworkCore;
using Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate.Models;
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
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using Senai.Vagas.Infrastructure.EntityTypeConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senai.Vagas.Infrastructure.Contexts
{
    public class SenaiVagasContext : DbContext, IUnitOfWork
    {
        // Nomes sempre no plural
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<StatusUsuario> StatusUsuarios { get; set; }
        public DbSet<HistoricoStatusEstagio> HistoricoStatusEstagios { get; set; }
        public DbSet<AlterarCredenciais> AlterarCredenciais { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<HistoricoStatusUsuario> HistoricoStatusUsuarios { get; set; }
        public DbSet<StatusEstagio> StatusEstagios { get; set; }
        public DbSet<FaixaSalarial> FaixaSalariais { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }
        public DbSet<RequerimentoMatricula> RequerimentoMatriculas { get; set; }
        public DbSet<StatusVaga> StatusVagas { get; set; }
        public DbSet<TermoOuEgressoAluno> TermoOuEgressoAlunos { get; set; }
        public DbSet<TipoAtividadeCnae> TipoAtividadeCnaes { get; set; }
        public DbSet<AtividadeCnae> AtividadeCnaes { get; set; }
        public DbSet<TipoCurso> TipoCursos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<QSA> QSAs { get; set; }
        public DbSet<TipoCnae> TipoCnaes  { get; set; }
        public DbSet<TipoEmpresa> TipoEmpresas { get; set; }
        public DbSet<TipoExperiencia> TipoExperiencias { get; set; }
        public DbSet<UsuarioAdministrador> UsuarioAdministradores { get; set; }
        public DbSet<UsuarioCandidatoAluno> UsuarioCandidatoAlunos { get; set; }
        public DbSet<PerfilUsuarioCandidatoAluno> PerfilUsuarioCandidatoAlunos { get; set; }
        public DbSet<AreasInteresseCandidatoAluno> AreasInteresseCandidatoAlunos { get; set; }
        public DbSet<UsuarioEmpresa> UsuarioEmpresas { get; set; }
        public DbSet<Vaga> Vagas { get; set; }
        public DbSet<AreaVagaRecomendada> AreaVagaRecomendadas { get; set; }
        public DbSet<ValidacaoUsuarioCandidato> ValidacaoUsuarioCandidatos { get; set; }
        public DbSet<Estagio> Estagios { get; set; }
        public DbSet<ContatoEstagio> ContatoEstagios { get; set; }
        public DbSet<PessoaResponsavel> PessoaResponsaveis { get; set; }
        public DbSet<ConviteEntrevista> ConviteEntrevistas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Municipio> Municipios { get; set; }
        public DbSet<UfSigla> UfSiglas { get; set; }
        public DbSet<Area> Areas { get; set; }


        public SenaiVagasContext(DbContextOptions<SenaiVagasContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Classes mapeadoras
            modelBuilder.ApplyConfiguration(new UsuarioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TipoUsuarioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusUsuarioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new HistoricoStatusUsuarioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TipoCursoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AlunoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EmpresaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioCandidatoAlunoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioEmpresaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioAdministradorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PerfilUsuarioCandidatoAlunoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TipoEmpresaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new QSAEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TipoCnaeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AtividadeCnaeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TipoAtividadeCnaeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EnderecoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MunicipioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UfSiglaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new VagaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusVagaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TipoExperienciaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FaixaSalarialEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AreaVagaRecomendadaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InscricaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConviteEntrevistaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ValidacaoUsuarioCandidatoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AlterarCredenciaisEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EstagioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaResponsavelEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ContatoEstagioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusEstagioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new HistoricoStatusEstagioEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TermoOuEgressoAlunoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RequerimentoMatriculaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AreaEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AreasInteresseCandidatoAlunoEntityTypeConfiguration());


            //Caso houver uma referencia de 2 Foreign Key's para uma mesma tabela, o loop altera o comportamento de Cascata para Restrito
            //[...] automaticamente ao fazer a migration
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

        }

        //Padrão de repositorios usando UnitOfWork, salva alterações na DB independentemente de quais tabelas foram alteradas.
        public async Task SaveDbChanges(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync();
        }
    }
}
