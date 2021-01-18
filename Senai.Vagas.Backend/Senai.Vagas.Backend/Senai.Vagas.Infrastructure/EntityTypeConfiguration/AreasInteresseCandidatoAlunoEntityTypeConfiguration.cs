using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.AreaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class AreasInteresseCandidatoAlunoEntityTypeConfiguration : IEntityTypeConfiguration<AreasInteresseCandidatoAluno>
    {
        public void Configure(EntityTypeBuilder<AreasInteresseCandidatoAluno> builder)
        {
            builder.ToTable("AreasInteressesCandidatosAlunos");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Area>()
                .WithMany()
                .HasForeignKey("AreaId")
                .IsRequired();

            builder.HasOne<UsuarioCandidatoAluno>()
                .WithMany()
                .HasForeignKey("UsuarioCandidatoAlunoId")
                .IsRequired();
        }
    }
}
