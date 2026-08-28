using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Exceptions;

namespace XmlValidador.Application.UseCases.ImportarXml
{
    public class ImportarXmlUseCase : IImportarXmlUseCase
    {
        private readonly IXmlNotaFiscalParser _parser;
        private readonly IValidadorNotaFiscal _validador;
        private readonly INotaFiscalRepository _repository;

        public ImportarXmlUseCase(
            IXmlNotaFiscalParser parser,
            IValidadorNotaFiscal validador,
            INotaFiscalRepository repository)
        {
            _parser = parser;
            _validador = validador;
            _repository = repository;
        }

        public async Task<ResultadoValidacaoDto> Executar(string xml)
        {
            var resultado = new ResultadoValidacaoDto();

            try
            {
                var notaFiscal = _parser.Parse(xml);

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

                if (!resultado.Erros.Any())
                {
                    var existe = await _repository
                        .ExistePorChaveAsync(
                            notaFiscal.ChaveAcesso.Valor);

                    if (existe)
                    {
                        resultado.Erros.Add(
                            new ErroValidacaoDto(
                                "NFE_DUPLICADA",
                                "A NF-e já foi importada."));
                    }
                }

                if (!resultado.Erros.Any())
                {
                    await _repository.AdicionarAsync(notaFiscal);
                }
            }
            catch (XmlInvalidoException ex)
            {
                resultado.Erros.Add(
                    new ErroValidacaoDto(
                        "XML_INVALIDO",
                        ex.Message));
            }

            resultado.Valido = !resultado.Erros.Any();

            return resultado;
        }
    }
}
