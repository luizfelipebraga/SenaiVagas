using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class TipoExperienciaEntityTypeConfiguration : IEntityTypeConfiguration<TipoExperiencia>
    {
        public void Configure(EntityTypeBuilder<TipoExperiencia> builder)
        {
            builder.ToTable("TiposExperiencias");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}
