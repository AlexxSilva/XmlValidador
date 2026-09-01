using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;
using XmlValidador.Domain.Exceptions;

namespace XmlValidador.Application.UseCases.ImportarXml
{
    public class ImportarXmlUseCase : IImportarXmlUseCase
    {
        private readonly IXmlNotaFiscalParser _parser;
        private readonly IValidadorNotaFiscal _validador;
        private readonly INotaFiscalRepository _repository;

        private readonly IHistoricoValidacaoRepository _historicoRepository;

        public ImportarXmlUseCase(
            IXmlNotaFiscalParser parser,
            IValidadorNotaFiscal validador,
            INotaFiscalRepository repository,
            IHistoricoValidacaoRepository historicoRepository)
        {
            _parser = parser;
            _validador = validador;
            _repository = repository;
            _historicoRepository = historicoRepository;
        }

        public async Task<ResultadoValidacaoDto> Executar(string xml)
        {
            var resultado = new ResultadoValidacaoDto();

            try
            {
                // Converte o XML em NotaFiscal
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

                // Verifica se a NF já existe
                var existe = await _repository
                    .ExistePorChaveAsync(notaFiscal.ChaveAcesso.Valor);

                if (existe)
                {
                    resultado.Erros.Add(
                        new ErroValidacaoDto(
                            "NFE_DUPLICADA",
                            "A NF-e já foi importada."));
                }
                else
                {
                    // Executa as validações
                    var resultadoValidacao = _validador.Validar(notaFiscal);

                    resultado.Erros.AddRange(resultadoValidacao.Erros);
                }

                // Salva o histórico da tentativa
                await SalvarHistoricoAsync(
                    notaFiscal.Id,
                    xml,
                    resultado);

                // Só importa a NF se estiver válida
                if (!resultado.Erros.Any())
                {
                    await _repository.AdicionarAsync(notaFiscal);
                }
            }
            catch (XmlInvalidoException ex)
            {
                var erro = new ErroValidacaoDto(
                    "XML_INVALIDO",
                    ex.Message);

                resultado.Erros.Add(erro);

                // XML inválido não possui NotaFiscal
                await SalvarHistoricoAsync(
                    null,
                    xml,
                    resultado);
            }

            resultado.Valido = !resultado.Erros.Any();

            return resultado;
        }

        private async Task SalvarHistoricoAsync(
    Guid? notaFiscalId,
    string xml,
    ResultadoValidacaoDto resultado)
        {
            var historico = new HistoricoValidacao(
                notaFiscalId,
                DateTime.Now,
                !resultado.Erros.Any(),
                xml);

            foreach (var erro in resultado.Erros)
            {
                var erroHistorico = new ErroHistoricoValidacao(
                    historico.Id,
                    erro.Codigo,
                    erro.Mensagem,
                    erro.Severidade);

                historico.AdicionarErro(erroHistorico);
            }

            await _historicoRepository.AdicionarAsync(historico);
        }
    }
}
