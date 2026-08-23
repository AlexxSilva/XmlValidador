using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;

namespace XmlValidador.Application.UseCases.ValidarXml
{
    public class ValidarXmlUseCase : IValidarXml
    {
        private readonly IXmlNotaFiscalParser _parser;

        public ValidarXmlUseCase(IXmlNotaFiscalParser parser)
        {
            _parser = parser;
        }

        public ResultadoValidarXmlDto ValidarXml(ValidarXmlRequestDto xml)
        {
            var notaFiscal = _parser.Parse(xml.Xml);
            return new ResultadoValidarXmlDto();
        }
    }
}
