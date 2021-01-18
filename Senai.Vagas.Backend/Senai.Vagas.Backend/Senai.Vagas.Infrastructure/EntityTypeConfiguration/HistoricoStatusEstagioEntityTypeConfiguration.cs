using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.EstagioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusEstagioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class HistoricoStatusEstagioEntityTypeConfiguration : IEntityTypeConfiguration<HistoricoStatusEstagio>
    {
        public void Configure(EntityTypeBuilder<HistoricoStatusEstagio> builder)
        {
            builder.ToTable("HistoricoStatusEstagios");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Explicacao).IsRequired();

            builder.HasOne<StatusEstagio>()
            .WithMany()
            .HasForeignKey("StatusEstagioId")
            .IsRequired();

            builder.HasOne<Estagio>()
            .WithMany()
            .HasForeignKey("EstagioId")
            .IsRequired();
        }
    }
}
