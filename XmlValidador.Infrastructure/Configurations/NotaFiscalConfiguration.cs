using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Infrastructure.Configurations
{
    public class NotaFiscalConfiguration : IEntityTypeConfiguration<NotaFiscal>
    {
        public void Configure(EntityTypeBuilder<NotaFiscal> builder)
        {
            builder.ToTable("NotasFiscais");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Numero)
                .IsRequired();

            builder.Property(x => x.Serie)
                .IsRequired();

            builder.Property(x => x.DataEmissao)
                .IsRequired();

            builder.ComplexProperty(x => x.ChaveAcesso, chave =>
            {
                chave.Property(x => x.Valor)
                    .HasColumnName("ChaveAcesso")
                    .HasMaxLength(44)
                    .IsRequired();
            });

            builder.ComplexProperty(x => x.ValorTotal, valor =>
            {
                valor.Property(x => x.Valor)
                    .HasColumnName("ValorTotal")
                    .HasPrecision(18, 2)
                    .IsRequired();
            });

            builder.HasOne(x => x.Empresa)
                .WithMany()
                .HasForeignKey("EmpresaId")
                .IsRequired();

            builder.HasMany(x => x.Itens)
                .WithOne()
                .HasForeignKey(x => x.NotaFiscalId);
        }
    }
}
