using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Domain.Entities
{
    public class NotaFiscal
    {
        public Guid Id { get; private set; }
        public int ChaveAcesso { get; private set; }
        public int Numero { get; private set; }
        public int Serie { get; private set; }
        public int DataEmissao { get; private set; }
        public int CnpjEmitente { get; private set; }
        public int CnpjDestinatario { get; private set; }
        public int ValorTotal { get; private set; }
        public int EmpresaId { get; private set; }


        public NotaFiscal(int chaveAcesso, int numero, int serie, 
            int dataEmissao, int cnpjEmitente, int cnpjDestinatario,
            int valorTotal, int empresaId)
        {
            Id =  new Guid();
            ChaveAcesso = chaveAcesso;
            Numero = numero;
            Serie = serie;
            DataEmissao = dataEmissao;
            CnpjEmitente = cnpjEmitente;
            CnpjDestinatario = cnpjDestinatario;
            ValorTotal = valorTotal;
            EmpresaId = empresaId;
        }
    }
}
