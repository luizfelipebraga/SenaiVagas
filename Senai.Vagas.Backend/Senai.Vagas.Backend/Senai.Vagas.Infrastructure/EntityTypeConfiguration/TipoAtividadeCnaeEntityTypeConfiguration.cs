using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class TipoAtividadeCnaeEntityTypeConfiguration : IEntityTypeConfiguration<TipoAtividadeCnae>
    {
        public void Configure(EntityTypeBuilder<TipoAtividadeCnae> builder)
        {
            builder.ToTable("TiposAtividadesCnaes");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}
