using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class ValidacaoUsuarioCandidatoEntityTypeConfiguration : IEntityTypeConfiguration<ValidacaoUsuarioCandidato>
    {
        public void Configure(EntityTypeBuilder<ValidacaoUsuarioCandidato> builder)
        {
            builder.ToTable("ValidacaoUsuarioCandidatos");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token).IsRequired();

            builder.HasOne<Aluno>()
            .WithMany()
            .HasForeignKey("AlunoId")
            .IsRequired();
            
        }
    }
}
