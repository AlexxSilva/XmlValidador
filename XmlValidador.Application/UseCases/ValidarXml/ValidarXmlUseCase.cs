using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace XmlValidador.Application.UseCases.ValidarXml
{
    public class ValidarXmlUseCase : IValidarXmlUseCase
    {
        private readonly IXmlNotaFiscalParser _parser;
        private readonly IEnumerable<IRegraValidacao> _regras;
        private readonly INotaFiscalRepository _repository;

        public ValidarXmlUseCase(IXmlNotaFiscalParser parser, 
                                IEnumerable<IRegraValidacao> regras,
                                INotaFiscalRepository repository)
        {
            _parser = parser;
            _regras = regras;
            _repository = repository;
        }


        //Orquestrar o processo de validação da nota.
        public async Task<ResultadoValidacaoDto> Executar(string xml)
        {
            var resultado = new ResultadoValidacaoDto();

            try
            {
                var notaFiscal = _parser.Parse(xml);

                foreach (var regra in _regras)
                {
                    var erro = regra.Validar(notaFiscal);

                    if (erro != null)
                        resultado.Erros.Add(new ErroValidacaoDto(
                        regra.Codigo,
                        erro));
                }

                if (!resultado.Erros.Any())
                {
                    await _repository.AdicionarAsync(notaFiscal);
                }

            }
            catch (XmlInvalidoException ex)
            {
                resultado.Erros.Add(new ErroValidacaoDto(
                        "XML_INVALIDO",
                        ex.Message));
            }

            resultado.Valido = !resultado.Erros.Any();

            return resultado;
        }
    }
}
