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
        public Cnpj CnpjEmitente { get; private set; }
        public Cnpj CnpjDestinatario { get; private set; }
        public ValorMonetario ValorTotal { get; private set; }
        public int EmpresaId { get; private set; }
        public DateTime DataCadastro { get; private set; }


        public NotaFiscal(ChaveAcessoNfe chaveAcesso, int numero, int serie,
            DateTime dataEmissao, Cnpj cnpjEmitente, Cnpj cnpjDestinatario,
            ValorMonetario valorTotal, int empresaId)
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
            DataCadastro = DateTime.Now;
        }
    }
}
