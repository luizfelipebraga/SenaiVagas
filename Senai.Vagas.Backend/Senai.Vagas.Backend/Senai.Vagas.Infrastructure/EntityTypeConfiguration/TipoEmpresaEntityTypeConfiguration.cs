using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.TipoEmpresaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class TipoEmpresaEntityTypeConfiguration : IEntityTypeConfiguration<TipoEmpresa>
    {
        public void Configure(EntityTypeBuilder<TipoEmpresa> builder)
        {
            builder.ToTable("TiposEmpresas");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}
