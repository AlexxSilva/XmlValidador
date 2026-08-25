using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;

namespace XmlValidador.Application.UseCases.ValidarXml
{
    public class ValidarXmlUseCase : IValidarXmlUseCase
    {
        private readonly IXmlNotaFiscalParser _parser;
        private readonly IEnumerable<IRegraValidacao> _regras;

        public ValidarXmlUseCase(IXmlNotaFiscalParser parser, 
                                IEnumerable<IRegraValidacao> regras)
        {
            _parser = parser;
            _regras = regras;
        }


        //Orquestrar o processo de validação da nota.
        public ResultadoValidacaoDto Executar(string xml)
        {
            var resultado = new ResultadoValidacaoDto();

            var notaFiscal = _parser.Parse(xml);

            foreach (var regra in _regras)
            {
                var erro = regra.Validar(notaFiscal);

                if (erro != null)
                    resultado.Erros.Add(erro);
            }

            resultado.Valido = !resultado.Erros.Any();

            return resultado;
        }
    }
}
