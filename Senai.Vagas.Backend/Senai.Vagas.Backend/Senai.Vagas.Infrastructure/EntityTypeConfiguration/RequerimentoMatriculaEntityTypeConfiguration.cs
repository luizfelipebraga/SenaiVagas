using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.RequerimentoMatriculaAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class RequerimentoMatriculaEntityTypeConfiguration : IEntityTypeConfiguration<RequerimentoMatricula>
    {
        public void Configure(EntityTypeBuilder<RequerimentoMatricula> builder)
        {
            builder.ToTable("RequerimentosMatriculas");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);
        }
    }
}
