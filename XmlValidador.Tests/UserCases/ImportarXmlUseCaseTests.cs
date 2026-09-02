using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;
using XmlValidador.Application.Services;
using XmlValidador.Application.UseCases.ImportarXml;
using XmlValidador.Domain.Entities;
using XmlValidador.Domain.Exceptions;
using XmlValidador.Domain.ValueObjects;

namespace XmlValidador.Tests.UserCases
{
    public class ImportarXmlUseCaseTests
    {
        [Fact]
        public async Task DeveRetornarErroQuandoNfJaEstiverImportada()
        {
            // Arrange

            var notaFiscal = CriarNotaFiscalValida();

            var parser = new ParserFakeXmlValido(notaFiscal);

            var regras = new List<IRegraValidacao>();

            var validador = new ValidadorNotaFiscal(regras);

            var notaFiscalRepository = new RepositoryNotaFiscalFake
            {
                Existe = true
            };

            var historicoRepository = new RepositoryFakeHistorico();

            var useCase = new ImportarXmlUseCase(
                parser,
                validador,
                notaFiscalRepository,
                historicoRepository);

            // Act

            var resultado = await useCase.Executar(
                "xml válido");

            // Assert


            Assert.Contains(
                resultado.Erros,
                erro => erro.Codigo == "NFE_DUPLICADA");

            Assert.False(resultado.Valido);
            Assert.False(notaFiscalRepository.Adicionou);
            Assert.True(historicoRepository.Adicionou);
        }

        [Fact]
        public async Task DeveImportarNfESalvarHistorico()
        {
            // Arrange

            var notaFiscal = CriarNotaFiscalValida();

            var parser = new ParserFakeXmlValido(notaFiscal);

            var regras = new List<IRegraValidacao>();

            var validador = new ValidadorNotaFiscal(regras);

            var notaFiscalRepository = new RepositoryNotaFiscalFake
            {
                Existe = false
            };

            var historicoRepository = new RepositoryFakeHistorico();

            var useCase = new ImportarXmlUseCase(
                parser,
                validador,
                notaFiscalRepository,
                historicoRepository);

            // Act

            var resultado = await useCase.Executar(
                "xml válido");

            // Assert

            Assert.True(resultado.Valido);
            Assert.True(notaFiscalRepository.Adicionou);
            Assert.True(historicoRepository.Adicionou);
        }

        [Fact]
        public async Task DeveRetornarErroDeValidacaoESalvarHistorico()
        {
            // Arrange

            var notaFiscal = CriarNotaFiscalValida();

            var parser = new ParserFakeXmlValido(notaFiscal);

            var regra = new RegraFakeComErro();

            var regras = new List<IRegraValidacao>
    {
        regra
    };

            var validador = new ValidadorNotaFiscal(regras);

            var notaFiscalRepository = new RepositoryNotaFiscalFake
            {
                Existe = false
            };

            var historicoRepository = new RepositoryFakeHistorico();

            var useCase = new ImportarXmlUseCase(
                parser,
                validador,
                notaFiscalRepository,
                historicoRepository);

            // Act

            var resultado = await useCase.Executar(
                "xml válido");

            // Assert

            Assert.False(resultado.Valido);

            Assert.Contains(
                resultado.Erros,
                erro => erro.Codigo == "ERRO_TESTE");

            Assert.False(notaFiscalRepository.Adicionou);

            Assert.True(historicoRepository.Adicionou);

            Assert.NotNull(historicoRepository.HistoricoSalvo);

            Assert.False(historicoRepository.HistoricoSalvo.Valido);

            Assert.Contains(
                historicoRepository.HistoricoSalvo.Erros,
                erro => erro.Codigo == "ERRO_TESTE");
        }

        [Fact]
        public async Task DeveRegistrarHistoricoQuandoXmlForInvalido()
        {
            // Arrange

            var parser = new ParserFakeXmlInvalido();

            var regras = new List<IRegraValidacao>();

            var validador = new ValidadorNotaFiscal(regras);

            var notaFiscalRepository = new RepositoryNotaFiscalFake();

            var historicoRepository = new RepositoryFakeHistorico();

            var useCase = new ImportarXmlUseCase(
                parser,
                validador,
                notaFiscalRepository,
                historicoRepository);

            // Act

            var resultado = await useCase.Executar(
                "xml inválido");

            // Assert

            Assert.False(resultado.Valido);

            Assert.Contains(
                resultado.Erros,
                erro => erro.Codigo == "XML_INVALIDO");

            Assert.False(notaFiscalRepository.Adicionou);

            Assert.True(historicoRepository.Adicionou);

            Assert.NotNull(historicoRepository.HistoricoSalvo);

            Assert.False(historicoRepository.HistoricoSalvo.Valido);

            Assert.Null(
                historicoRepository.HistoricoSalvo.NotaFiscalId);
        }

        private NotaFiscal CriarNotaFiscalValida()
        {
            var empresa = new Empresa(
                "Empresa Teste",
                "Empresa Teste",
                new Cnpj("12345678000195"),
                "123456789",
                "Rua Teste",
                "100",
                "Centro",
                "1234567",
                "São Paulo",
                "SP",
                "01000000",
                "1058",
                "Brasil"
            );

            var notaFiscal = new NotaFiscal(
                new ChaveAcessoNfe(
                    "35260812345678000195550010000000011000000010"),
                1,
                1,
                DateTime.Now,
                empresa,
                new ValorMonetario(150m)
            );

            var item = new ItemNotaFiscal(
                notaFiscal.Id,
                1,
                "001",
                "Produto Teste",
                "12345678",
                1m,
                new ValorMonetario(150m),
                new ValorMonetario(150m)
            );

            notaFiscal.AdicionarItem(item);


            return notaFiscal;
        }
    }

    public class RepositoryNotaFiscalFake : INotaFiscalRepository
    {
        public bool Existe { get; set; }

        public bool Adicionou { get; private set; }

        public Task<bool> ExistePorChaveAsync(string chaveAcesso)
        {
            return Task.FromResult(Existe);
        }

        public Task AdicionarAsync(NotaFiscal notaFiscal)
        {
            Adicionou = true;

            return Task.CompletedTask;
        }
    }
    public class ParserFakeXmlValido : IXmlNotaFiscalParser
    {
        private readonly NotaFiscal _notaFiscal;

        public ParserFakeXmlValido(NotaFiscal notaFiscal)
        {
            _notaFiscal = notaFiscal;
        }

        public NotaFiscal Parse(string xml)
        {
            return _notaFiscal;
        }
    }
    public class RepositoryFakeHistorico : IHistoricoValidacaoRepository
    {
        public bool Adicionou { get; private set; }

        public HistoricoValidacao? HistoricoSalvo { get; private set; }

        public Task AdicionarAsync(HistoricoValidacao historicoValidacao)
        {
            Adicionou = true;
            HistoricoSalvo = historicoValidacao;

            return Task.CompletedTask;
        }
    }
    public class RegraFakeComErro : IRegraValidacao
    {
        public string Codigo => "ERRO_TESTE";

        public string? Validar(NotaFiscal notaFiscal)
        {
            return "Erro gerado apenas para teste.";
        }
    }

    public class ParserFakeXmlInvalido : IXmlNotaFiscalParser
    {
        public NotaFiscal Parse(string xml)
        {
            throw new XmlInvalidoException(
                "XML inválido para teste.");
        }
    }

}
