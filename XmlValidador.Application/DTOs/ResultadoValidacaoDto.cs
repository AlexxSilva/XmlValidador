using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Application.DTOs
{
    public class ResultadoValidacaoDto
    {
        public bool Valido { get; set; }
        public NotaFiscalValidacaoDto? NotaFiscal { get; set; }
        public List<ErroValidacaoDto> Erros { get; set; } = new();
    }
}
