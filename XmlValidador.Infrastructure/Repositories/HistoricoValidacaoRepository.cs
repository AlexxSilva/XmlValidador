using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;
using XmlValidador.Infrastructure.Data;

namespace XmlValidador.Infrastructure.Repositories
{
    public class HistoricoValidacaoRepository : IHistoricoValidacaoRepository
    {
        private readonly XmlValidadorDbContext _context;

        public HistoricoValidacaoRepository(XmlValidadorDbContext context)
        {
            _context = context; 
        }

        public Task AdicionarAsync(HistoricoValidacao historicoValidacao)
        {
            _context.HistoricosValidacao.Add(historicoValidacao);
            return _context.SaveChangesAsync();
        }
    }
}
