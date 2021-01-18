using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class PerfilUsuarioCandidatoAlunoEntityTypeConfiguration : IEntityTypeConfiguration<PerfilUsuarioCandidatoAluno>
    {
        public void Configure(EntityTypeBuilder<PerfilUsuarioCandidatoAluno> builder)
        {
            builder.ToTable("PerfilUsuarioCandidatoAlunos");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<UsuarioCandidatoAluno>()
            .WithMany()
            .HasForeignKey("UsuarioCandidatoAlunoId")
            .IsRequired();

        }
    }
}
