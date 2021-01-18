using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class QSAEntityTypeConfiguration : IEntityTypeConfiguration<QSA>
    {
        public void Configure(EntityTypeBuilder<QSA> builder)
        {
            builder.ToTable("QSAs");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Empresa>()
                .WithMany()
                .HasForeignKey("EmpresaId")
                .IsRequired();
        }
    }
}
