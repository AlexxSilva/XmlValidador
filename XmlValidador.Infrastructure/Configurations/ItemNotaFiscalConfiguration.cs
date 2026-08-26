using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Infrastructure.Configurations
{
    public class ItemNotaFiscalConfiguration
        : IEntityTypeConfiguration<ItemNotaFiscal>
    {
        public void Configure(EntityTypeBuilder<ItemNotaFiscal> builder)
        {
            builder.ToTable("ItensNotaFiscal");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CodigoProduto)
                .HasMaxLength(100);

            builder.Property(x => x.Descricao)
                .HasMaxLength(500);

            builder.Property(x => x.Quantidade)
                .HasPrecision(18, 4);

            builder.ComplexProperty(x => x.ValorUnitario, valor =>
            {
                valor.Property(x => x.Valor)
                    .HasColumnName("ValorUnitario")
                    .HasPrecision(18, 2)
                    .IsRequired();
            });

            builder.ComplexProperty(x => x.ValorTotal, valor =>
            {
                valor.Property(x => x.Valor)
                    .HasColumnName("ValorTotal")
                    .HasPrecision(18, 2)
                    .IsRequired();
            });
        }
    }
}
