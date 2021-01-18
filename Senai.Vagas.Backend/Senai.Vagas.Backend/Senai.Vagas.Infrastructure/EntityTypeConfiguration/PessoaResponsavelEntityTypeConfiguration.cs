using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Senai.Vagas.Domain.AggregatesModel.EstagioAggregate.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace Senai.Vagas.Infrastructure.EntityTypeConfiguration
{
    public class PessoaResponsavelEntityTypeConfiguration : IEntityTypeConfiguration<PessoaResponsavel>
    {
        public void Configure(EntityTypeBuilder<PessoaResponsavel> builder)
        {
            builder.ToTable("PessoaResponsaveis");

            builder.Property(x => x.Id).IsRequired();
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome).IsRequired();
        }
    }
}
