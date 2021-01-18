using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class UfSiglaEntityTypeConfiguration : IEntityTypeConfiguration<UfSigla>
    {
        public void Configure(EntityTypeBuilder<UfSigla> builder)
        {
            builder.ToTable("UfSiglas");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UFEstado).IsRequired();
            builder.Property(x => x.UFSigla).IsRequired();
        }
    }
}
