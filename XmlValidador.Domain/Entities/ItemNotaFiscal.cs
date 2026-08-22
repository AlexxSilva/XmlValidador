using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.ValueObjects;

namespace XmlValidador.Domain.Entities
{
    public class ItemNotaFiscal
    {
   
        public Guid Id { get; private set; }
        public int NotaFiscalId { get; private set; }
        public int Nitem { get; private set; }
        public string? CodigoProduto { get; private set; }
        public string? Descricao { get; private set; }
        public int Quantidade { get; private set; }
        public ValorMonetario ValorUnitario { get; private set; }
        public ValorMonetario ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public ItemNotaFiscal(int notaFiscalId, int nitem, string? codigoProduto, 
            string? descricao, int quantidade, ValorMonetario valorUnitario, ValorMonetario valorTotal)
        {
            Id = new Guid();
            NotaFiscalId = notaFiscalId;
            Nitem = nitem;
            CodigoProduto = codigoProduto;
            Descricao = descricao;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
            ValorTotal = valorTotal;
            DataCadastro = DateTime.Now;
        }

    }
}
