using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.EstagioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class EstagioEntityTypeConfiguration : IEntityTypeConfiguration<Estagio>
    {
        public void Configure(EntityTypeBuilder<Estagio> builder)
        {
            builder.ToTable("Estagios");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<UsuarioEmpresa>()
            .WithMany()
            .HasForeignKey("UsuarioEmpresaId")
            .IsRequired();

            builder.HasOne<TermoOuEgressoAluno>()
            .WithMany()
            .HasForeignKey("TermoOuEgressoAlunoId")
            .IsRequired();

            builder.HasOne<Aluno>()
            .WithMany()
            .HasForeignKey("AlunoId")
            .IsRequired();

            builder.HasOne<PessoaResponsavel>()
            .WithMany()
            .HasForeignKey("PessoaResponsavelId")
            .IsRequired();
            

            builder.HasOne<RequerimentoMatricula>()
            .WithMany()
            .HasForeignKey("RequerimentoMatriculaId")
            .IsRequired();


            builder.HasOne<Endereco>()
            .WithMany()
            .HasForeignKey("EnderecoId")
            .IsRequired();
        }
    }
}
