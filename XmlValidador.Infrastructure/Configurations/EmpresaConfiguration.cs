using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Infrastructure.Configurations
{
    public class EmpresaConfiguration : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.ToTable("Empresas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.RazaoSocial)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.NomeFantasia)
                .HasMaxLength(200);

            builder.Property(x => x.InscricaoEstadual)
                .HasMaxLength(20);

            builder.ComplexProperty(x => x.Cnpj, cnpj =>
            {
                cnpj.Property(x => x.Valor)
                    .HasColumnName("Cnpj")
                    .HasMaxLength(14)
                    .IsRequired();
            });
        }
    }
}
