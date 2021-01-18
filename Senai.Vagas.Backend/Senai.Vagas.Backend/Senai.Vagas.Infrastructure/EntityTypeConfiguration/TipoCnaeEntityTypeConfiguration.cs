using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class TipoCnaeEntityTypeConfiguration : IEntityTypeConfiguration<TipoCnae>
    {
        public void Configure(EntityTypeBuilder<TipoCnae> builder)
        {
            builder.ToTable("TipoCnaes");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}
