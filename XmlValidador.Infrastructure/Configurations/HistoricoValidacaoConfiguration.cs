using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Infrastructure.Configurations
{
    public class HistoricoValidacaoConfiguration: IEntityTypeConfiguration<HistoricoValidacao>
    {
        public void Configure(EntityTypeBuilder<HistoricoValidacao> builder)
        {
            builder.ToTable("HistoricosValidacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataValidacao)
                .IsRequired();

            builder.Property(x => x.Valido)
                .IsRequired();

            builder.Property(x => x.Xml)
                .IsRequired();

            builder.HasOne(x => x.NotaFiscal)
                .WithMany(x => x.Historicos)
                .HasForeignKey(x => x.NotaFiscalId)
                .IsRequired(false);
        }
    }
}
