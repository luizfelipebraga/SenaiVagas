using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.InscricaoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class InscricaoEntityTypeConfiguration : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            builder.ToTable("Inscricoes");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Vaga>()
            .WithMany()
            .HasForeignKey("VagaId")
            .IsRequired();

            builder.HasOne<UsuarioCandidatoAluno>()
            .WithMany()
            .HasForeignKey("UsuarioCandidatoAlunoId")
            .IsRequired();
        }
    }
}
