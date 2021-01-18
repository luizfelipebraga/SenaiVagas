using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class UsuarioCandidatoAlunoEntityTypeConfiguration : IEntityTypeConfiguration<UsuarioCandidatoAluno>
    {
        public void Configure(EntityTypeBuilder<UsuarioCandidatoAluno> builder)
        {
            builder.ToTable("UsuarioCandidatoAlunos");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Aluno>()
            .WithMany()
            .HasForeignKey("AlunoId")
            .IsRequired();

            builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey("UsuarioId")
            .IsRequired();
        }
    }
}
