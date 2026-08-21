using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Domain.Entities
{
    public class ItemNotaFiscal
    {
        public int Id { get; set; }
        public int NotaFiscalId { get; set; }
        public int Nitem { get; set; }
        public int CodigoProduto { get; set; }
        public int Descricao { get; set; }
        public int Quantidade { get; set; }
        public int ValorUnitario { get; set; }
        public int ValorTotal { get; set; }
    }
}
