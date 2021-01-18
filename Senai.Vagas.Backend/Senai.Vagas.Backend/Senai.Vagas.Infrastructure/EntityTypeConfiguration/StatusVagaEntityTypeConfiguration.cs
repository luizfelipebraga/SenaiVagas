using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class StatusVagaEntityTypeConfiguration : IEntityTypeConfiguration<StatusVaga>
    {
        public void Configure(EntityTypeBuilder<StatusVaga> builder)
        {
            builder.ToTable("StatusVagas");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}
