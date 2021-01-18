using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.TipoUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class UsuarioEntityTypeConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.Senha).IsRequired();
            builder.Property(x => x.Nome).IsRequired();

            
            builder.HasOne<TipoUsuario>()
                .WithMany()
                .HasForeignKey("TipoUsuarioId")
                .IsRequired();
        }
    }
}
