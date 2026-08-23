using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.ValueObjects;

namespace XmlValidador.Domain.Entities
{
    public class ItemNotaFiscal
    {
   
        public Guid Id { get; private set; }
        public Guid NotaFiscalId { get; private set; }
        public int Nitem { get; private set; }
        public string? CodigoProduto { get; private set; }
        public string? Descricao { get; private set; }
        public decimal Quantidade { get; private set; }
        public ValorMonetario ValorUnitario { get; private set; }
        public ValorMonetario ValorTotal { get; private set; }
        public DateTime DataCadastro { get; private set; }

        public ItemNotaFiscal(Guid notaFiscalId,int nitem,string? codigoProduto,string? descricao,
        decimal quantidade,ValorMonetario valorUnitario,ValorMonetario valorTotal)
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
