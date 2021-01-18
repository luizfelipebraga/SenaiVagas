using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.EmpresaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TipoAtividadeCnaeAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class AtividadeCnaeEntityTypeConfiguration : IEntityTypeConfiguration<AtividadeCnae>
    {
        public void Configure(EntityTypeBuilder<AtividadeCnae> builder)
        {
            builder.ToTable("AtividadeCnaes");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<TipoCnae>()
                .WithMany()
                .HasForeignKey("TipoCnaeId")
                .IsRequired();

            builder.HasOne<TipoAtividadeCnae>()
                .WithMany()
                .HasForeignKey("TipoAtividadeCnaeId")
                .IsRequired();

            builder.HasOne<Empresa>()
                .WithMany()
                .HasForeignKey("EmpresaId")
                .IsRequired();


        }
    }
}
