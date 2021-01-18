using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.TermoOuEgressoAlunoEstagioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class TermoOuEgressoAlunoEntityTypeConfiguration : IEntityTypeConfiguration<TermoOuEgressoAluno>
    {
        public void Configure(EntityTypeBuilder<TermoOuEgressoAluno> builder)
        {
            builder.ToTable("TermosOuEgressosAlunos");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}
