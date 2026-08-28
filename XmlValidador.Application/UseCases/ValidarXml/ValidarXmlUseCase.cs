using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;
using XmlValidador.Application.Services;
using XmlValidador.Domain.Exceptions;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace XmlValidador.Application.UseCases.ValidarXml
{
    public class ValidarXmlUseCase : IValidarXmlUseCase
    {
        private readonly IXmlNotaFiscalParser _parser;
        private readonly IValidadorNotaFiscal _validador;

        public ValidarXmlUseCase(IXmlNotaFiscalParser parser, 
                                IValidadorNotaFiscal validador)
        {
            _parser = parser;
            _validador = validador;
        }


        //Orquestrar o processo de validação da nota.
            public async Task<ResultadoValidacaoDto> Executar(string xml)
            {
                var resultado = new ResultadoValidacaoDto();

                try
                {
                    var notaFiscal = _parser.Parse(xml);

                    // Informações da NF para retornar na resposta
                    resultado.NotaFiscal = new NotaFiscalValidacaoDto
                    {
                        ChaveAcesso = notaFiscal.ChaveAcesso.Valor,
                        Numero = notaFiscal.Numero,
                        Serie = notaFiscal.Serie,
                        DataEmissao = notaFiscal.DataEmissao,
                        CnpjEmitente = notaFiscal.Empresa.Cnpj.Valor,
                        ValorTotal = notaFiscal.ValorTotal.Valor
                    };


                var resultadoValidacao = _validador.Validar(notaFiscal);

                resultado.Erros.AddRange(resultadoValidacao.Erros);


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
