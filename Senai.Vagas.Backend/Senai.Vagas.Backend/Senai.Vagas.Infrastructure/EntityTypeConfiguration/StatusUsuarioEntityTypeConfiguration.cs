using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class StatusUsuarioEntityTypeConfiguration : IEntityTypeConfiguration<StatusUsuario>
    {
        public void Configure(EntityTypeBuilder<StatusUsuario> builder)
        {
            builder.ToTable("StatusUsuarios");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}
