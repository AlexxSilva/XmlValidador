using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Application.Interfaces
{
    public interface IXmlNotaFiscalParser
    {
        NotaFiscal Parse(string xml);
    }
}
