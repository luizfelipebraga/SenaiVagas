using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAdminstradorAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class UsuarioAdministradorEntityTypeConfiguration : IEntityTypeConfiguration<UsuarioAdministrador>
    {
        public void Configure(EntityTypeBuilder<UsuarioAdministrador> builder)
        {
            builder.ToTable("UsuarioAdministradores");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey("UsuarioId")
            .IsRequired();
        }
    }
}
