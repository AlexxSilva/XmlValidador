using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Infrastructure.Data
{
    public class XmlValidadorDbContext : DbContext
    {
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<NotaFiscal> NotasFiscais { get; set; }
        public DbSet<ItemNotaFiscal> ItensNotaFiscal { get; set; }
        public DbSet<HistoricoValidacao> HistoricosValidacao { get; set; }
        public DbSet<ErroHistoricoValidacao> ErrosHistoricoValidacao { get; set; }

        public XmlValidadorDbContext(DbContextOptions<XmlValidadorDbContext> options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(XmlValidadorDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
