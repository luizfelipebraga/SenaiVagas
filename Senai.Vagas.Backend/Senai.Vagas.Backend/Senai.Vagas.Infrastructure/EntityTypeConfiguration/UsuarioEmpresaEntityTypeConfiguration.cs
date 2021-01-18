using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class UsuarioEmpresaEntityTypeConfiguration : IEntityTypeConfiguration<UsuarioEmpresa>
    {
        public void Configure(EntityTypeBuilder<UsuarioEmpresa> builder)
        {
            builder.ToTable("UsuarioEmpresas");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Usuario>()
           .WithMany()
           .HasForeignKey("UsuarioId")
           .IsRequired();

            builder.HasOne<Empresa>()
           .WithMany()
           .HasForeignKey("EmpresaId")
           .IsRequired();
        }
    }
}
