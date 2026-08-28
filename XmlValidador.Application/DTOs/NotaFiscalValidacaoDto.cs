using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Application.DTOs
{
    public class NotaFiscalValidacaoDto
    {
        public string ChaveAcesso { get; set; }
        public int Numero { get; set; }
        public int Serie { get; set; }
        public DateTime DataEmissao { get; set; }
        public string CnpjEmitente { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
