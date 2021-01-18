using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class FaixaSalarialEntityTypeConfiguration : IEntityTypeConfiguration<FaixaSalarial>
    {
        public void Configure(EntityTypeBuilder<FaixaSalarial> builder)
        {
            builder.ToTable("FaixasSalariais");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}
