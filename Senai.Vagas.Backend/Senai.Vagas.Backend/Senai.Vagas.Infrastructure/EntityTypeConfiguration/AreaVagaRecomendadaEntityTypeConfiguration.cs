using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.AreaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TipoCursoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class AreaVagaRecomendadaEntityTypeConfiguration : IEntityTypeConfiguration<AreaVagaRecomendada>
    {
        public void Configure(EntityTypeBuilder<AreaVagaRecomendada> builder)
        {
            builder.ToTable("AreasVagasRecomendadas");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Area>()
                .WithMany()
                .HasForeignKey("AreaId")
                .IsRequired();

            builder.HasOne<Vaga>()
                .WithMany()
                .HasForeignKey("VagaId")
                .IsRequired();
        }
    }
}
