using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Domain.Entities
{
    public class ValidacaoNotaFiscal
    {
        public int Id { get; set; }

        public int NotaFiscalId { get; set; }
        public int Codigo { get; set; }
        public int Descricao { get; set; }
        public int Severidade { get; set; }
        public int Status { get; set; }
        public int Evidencia { get; set; }

    }
}
