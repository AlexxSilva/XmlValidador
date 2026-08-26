using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;
using XmlValidador.Infrastructure.Data;

namespace XmlValidador.Infrastructure.Repositories
{
    public class NotaFiscalRepository : INotaFiscalRepository
    {
        private readonly XmlValidadorDbContext _context;

        public NotaFiscalRepository(XmlValidadorDbContext context)
        {
            _context = context; 
        }

        public async Task AdicionarAsync(NotaFiscal notaFiscal)
        {
            _context.NotasFiscais.Add(notaFiscal);
            await _context.SaveChangesAsync();
        }
    }
}
