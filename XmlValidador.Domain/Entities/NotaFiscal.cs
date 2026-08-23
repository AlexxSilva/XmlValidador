using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.ValueObjects;

namespace XmlValidador.Domain.Entities
{
    public class NotaFiscal
    {
        private readonly List<ItemNotaFiscal> _itens = new();

        public Guid Id { get; private set; }

        public ChaveAcessoNfe ChaveAcesso { get; private set; }

        public int Numero { get; private set; }

        public int Serie { get; private set; }

        public DateTime DataEmissao { get; private set; }

        public Empresa Empresa { get; private set; }

        public ValorMonetario ValorTotal { get; private set; }

        public IReadOnlyCollection<ItemNotaFiscal> Itens =>
            _itens.AsReadOnly();

        public NotaFiscal(
            ChaveAcessoNfe chaveAcesso,
            int numero,
            int serie,
            DateTime dataEmissao,
            Empresa empresa,
            ValorMonetario valorTotal)
        {
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
            _itens.Add(item);
        }
    }
}
