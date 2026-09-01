using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.ValueObjects;

namespace XmlValidador.Domain.Entities
{
    public class NotaFiscal
    {
        
        public Guid Id { get; private set; }

        public ChaveAcessoNfe ChaveAcesso { get; private set; }

        public int Numero { get; private set; }

        public int Serie { get; private set; }

        public DateTime DataEmissao { get; private set; }

        public Empresa Empresa { get; private set; }

        public ValorMonetario ValorTotal { get; private set; }


        private readonly List<ItemNotaFiscal> _itens = new();
        public IReadOnlyCollection<ItemNotaFiscal> Itens =>
            _itens.AsReadOnly();

        private readonly List<HistoricoValidacao> _historicos = new();

        public IReadOnlyCollection<HistoricoValidacao> Historicos =>
        _historicos.AsReadOnly();

        private NotaFiscal()
        {
        }

        public NotaFiscal(
            ChaveAcessoNfe chaveAcesso,
            int numero,
            int serie,
            DateTime dataEmissao,
            Empresa empresa,
            ValorMonetario valorTotal)
        {

            if (numero <= 0)
                throw new ArgumentException(
                    "O número da NF-e deve ser maior que zero.");

            if (serie <= 0)
                throw new ArgumentException(
                    "A série da NF-e deve ser maior que zero.");

            if (empresa == null)
                throw new ArgumentNullException(nameof(empresa));

            Id = Guid.NewGuid();
            ChaveAcesso = chaveAcesso;
            Numero = numero;
            Serie = serie;
            DataEmissao = dataEmissao;
            Empresa = empresa;
            ValorTotal = valorTotal;
        }

        public void AdicionarItem(ItemNotaFiscal item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _itens.Add(item);
        }

        public void AdicionarHistorico(HistoricoValidacao historico)
        {
            if (historico == null)
                throw new ArgumentNullException(nameof(historico));

            _historicos.Add(historico);
        }
    }
}
