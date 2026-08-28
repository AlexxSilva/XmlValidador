using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Interfaces;
using XmlValidador.Application.Services;
using XmlValidador.Application.UseCases.ImportarXml;
using XmlValidador.Domain.Entities;
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

            var repository = new RepositoryFake
            {
                Existe = true
            };

            var useCase = new ImportarXmlUseCase(
                parser,
                validador,
                repository);

            // Act

            var resultado = await useCase.Executar(
                "xml válido");

            // Assert

            Assert.False(resultado.Valido);

            Assert.Contains(
                resultado.Erros,
                erro => erro.Codigo == "NFE_DUPLICADA");

            Assert.False(repository.Adicionou);
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
                1m,
                new ValorMonetario(150m),
                new ValorMonetario(150m)
            );

            notaFiscal.AdicionarItem(item);

            return notaFiscal;
        }
    }


    public class RepositoryFake : INotaFiscalRepository
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
}
