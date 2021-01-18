using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.ConviteEntrevistaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.EnderecoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.StatusVagaAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.UsuarioCandidatoAlunoAggregate.Models;
using Senai.Vagas.Domain.AggregatesModel.VagaAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class ConviteEntrevistaEntityTypeConfiguration : IEntityTypeConfiguration<ConviteEntrevista>
    {
        public void Configure(EntityTypeBuilder<ConviteEntrevista> builder)
        {
            builder.ToTable("ConvitesEntrevistas");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.HasOne<Endereco>()
                .WithMany()
                .HasForeignKey("EnderecoId")
                .IsRequired();


            builder.HasOne<UsuarioCandidatoAluno>()
                .WithMany()
                .HasForeignKey("UsuarioCandidatoAlunoId")
                .IsRequired();

            builder.HasOne<Vaga>()
                .WithMany()
                .HasForeignKey("VagaId")
                .IsRequired();
        }
    }
}



