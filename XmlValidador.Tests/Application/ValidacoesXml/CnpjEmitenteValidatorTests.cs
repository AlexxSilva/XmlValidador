using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.ValidacoesXml;
using XmlValidador.Domain.Entities;
using XmlValidador.Domain.ValueObjects;

namespace XmlValidador.Tests.Application.ValidacoesXml
{
    public class CnpjEmitenteValidatorTests
    {

        [Fact]
        public void DeveRetornarNullQuandoCnpjEstiverInformado()
        {
            // Arrange
            var chave = new ChaveAcessoNfe("12345678901234567890123456789012345678901234");

            var empresa = new Empresa("Empresa Teste", "Empresa", new Cnpj("12345678000199"), "0000000000",
            "Rua teste", "1", "Bairro do teste", "0000", "São Paulo", "SP", "0000000", "1055", "Brasil");

            var nota = new NotaFiscal(chave, 1, 1, DateTime.Now, empresa, new ValorMonetario(100));

            var validator = new CnpjEmitenteValidator();

            // Act
            var resultado = validator.Validar(nota);
            // Assert
            Assert.Null(resultado);
        }
        [Fact]
        public void DeveRetornarCNPJemitenteNaoinformadoQuandoCnpjNaoForInformado()
        {
            // Arrange
            var chave = new ChaveAcessoNfe("12345678901234567890123456789012345678901234");

            var empresa = new Empresa("Empresa Teste", "Empresa", null, "0000000000",
           "Rua teste", "1", "Bairro do teste", "0000", "São Paulo", "SP", "0000000", "1055", "Brasil");

            var nota = new NotaFiscal(chave, 1, 1, DateTime.Now, empresa, new ValorMonetario(100));

            var validator = new CnpjEmitenteValidator();

            // Act
            var resultado = validator.Validar(nota);

            // Assert
            Assert.Equal("CNPJ do emitente não informado.", resultado);
        }
    }
}
