using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Application.DTOs
{
    public class ResultadoValidacaoDto
    {
        public bool Valido { get; set; }
        public List<string> Erros { get; set; } = new();
    }
}
