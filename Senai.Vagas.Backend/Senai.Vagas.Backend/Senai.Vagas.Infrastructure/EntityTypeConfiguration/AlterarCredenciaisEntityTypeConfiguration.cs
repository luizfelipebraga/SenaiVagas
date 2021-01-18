using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class AlterarCredenciaisEntityTypeConfiguration : IEntityTypeConfiguration<AlterarCredenciais>
    {
        public void Configure(EntityTypeBuilder<AlterarCredenciais> builder)
        {
            builder.ToTable("AlterarCredenciais");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Usuario>()
                .WithMany()
                .HasForeignKey("UsuarioId")
                .IsRequired();
        }
    }
}
