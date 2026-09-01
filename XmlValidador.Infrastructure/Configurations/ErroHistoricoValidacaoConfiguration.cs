using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Infrastructure.Configurations
{
    public class ErroHistoricoValidacaoConfiguration :
            IEntityTypeConfiguration<ErroHistoricoValidacao>
    {
        public void Configure(EntityTypeBuilder<ErroHistoricoValidacao> builder)
        {
            builder.ToTable("ErrosHistoricoValidacao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Codigo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Mensagem)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(x => x.Severidade)
                .IsRequired();

            builder.HasOne(x => x.HistoricoValidacao)
                .WithMany(x => x.Erros)
                .HasForeignKey(x => x.HistoricoValidacaoId)
                .IsRequired();
        }
    }
}
