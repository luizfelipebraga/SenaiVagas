using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.HistoricoStatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusUsuarioAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class HistoricoStatusUsuarioEntityTypeConfiguration : IEntityTypeConfiguration<HistoricoStatusUsuario>
    {
        public void Configure(EntityTypeBuilder<HistoricoStatusUsuario> builder)
        {
            builder.ToTable("HistoricoStatusUsuarios");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Usuario>()
            .WithMany()
            .HasForeignKey("UsuarioId")
            .IsRequired();

            builder.HasOne<StatusUsuario>()
            .WithMany()
            .HasForeignKey("StatusUsuarioId")
            .IsRequired();
        }
    }
}
