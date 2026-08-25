using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Domain.Exceptions
{
    public class XmlInvalidoException : Exception
    {
        public XmlInvalidoException(string mensagem)
        : base(mensagem)
        {
        }
    }
}
