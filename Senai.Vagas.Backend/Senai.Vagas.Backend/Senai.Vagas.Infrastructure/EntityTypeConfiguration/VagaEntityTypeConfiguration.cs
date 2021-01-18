using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.FaixaSalarialAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.TipoExperienciaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioEmpresaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class VagaEntityTypeConfiguration : IEntityTypeConfiguration<Vaga>
    {
        public void Configure(EntityTypeBuilder<Vaga> builder)
        {
            builder.ToTable("Vagas");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<StatusVaga>()
                .WithMany()
                .HasForeignKey("StatusVagaId")
                .IsRequired();

            builder.HasOne<TipoExperiencia>()
                .WithMany()
                .HasForeignKey("TipoExperienciaId")
                .IsRequired();

            builder.HasOne<UsuarioEmpresa>()
                .WithMany()
                .HasForeignKey("UsuarioEmpresaId")
                .IsRequired();

            builder.HasOne<Municipio>()
                .WithMany()
                .HasForeignKey("MunicipioId")
                .IsRequired();

            builder.HasOne<FaixaSalarial>()
                .WithMany()
                .HasForeignKey("FaixaSalarialId")
                .IsRequired();
        }
    }
}
