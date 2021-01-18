using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.EstagioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class ContatoEstagioEntityTypeConfiguration : IEntityTypeConfiguration<ContatoEstagio>
    {
        public void Configure(EntityTypeBuilder<ContatoEstagio> builder)
        {
            builder.ToTable("ContatoEstagios");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TelefoneOuEmail).IsRequired();

            builder.HasOne<Estagio>()
                .WithMany()
                .HasForeignKey("EstagioId")
                .IsRequired();
        }
    }
}
