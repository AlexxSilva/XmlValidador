using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Enums;

namespace XmlValidador.Application.DTOs
{
    public class ErroValidacaoDto
    {
        public string Codigo   { get; set; }
        public string Mensagem { get; set; }

        public SeveridadeValidacao Severidade { get; set; }

        public ErroValidacaoDto(string codigo, string mensagem)
        {
            Codigo = codigo;
            Mensagem = mensagem;
        }
    }
}
