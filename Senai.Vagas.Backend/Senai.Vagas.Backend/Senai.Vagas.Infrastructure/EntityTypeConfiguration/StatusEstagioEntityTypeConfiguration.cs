using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.StatusEstagioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class StatusEstagioEntityTypeConfiguration : IEntityTypeConfiguration<StatusEstagio>
    {
        public void Configure(EntityTypeBuilder<StatusEstagio> builder)
        {
            builder.ToTable("StatusEstagios");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}

