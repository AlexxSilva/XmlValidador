using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.ValidacoesXml;
using XmlValidador.Domain.Entities;
using XmlValidador.Domain.ValueObjects;

namespace XmlValidador.Tests.Application.ValidacoesXml
{
    public class TotalItensValidatorTests
    {
        [Fact]
        public void RetornaMensagemDeErroQuandoSomaDosItensNaoCorrespondeAoValorTotal()
        {
            // Arrange
            var chave = new ChaveAcessoNfe("12345678901234567890123456789012345678901234");

            var empresa = new Empresa("Empresa Teste", "Empresa", new Cnpj("12345678000199"), "0000000000",
            "Rua teste", "1", "Bairro do teste", "0000", "São Paulo", "SP", "0000000", "1055", "Brasil");

            var nota = new NotaFiscal(chave, 1, 1, DateTime.Now, empresa, new ValorMonetario(100));

            nota.AdicionarItem(new ItemNotaFiscal(nota.Id, 1, "00001", "Produto Teste", 1, new ValorMonetario(100), new ValorMonetario(101)));


            var validator = new TotalItensValidator();
            // Act
            var resultado = validator.Validar(nota);
            // Assert
            Assert.Equal("A soma dos itens (101,00) não corresponde ao valor total da NF-e (100,00).", resultado);
        }

        [Fact]
        public void RetornaNullQuandoSomaDosItensCorrespondeAoValorTotal()
        {
            // Arrange
            var chave = new ChaveAcessoNfe("12345678901234567890123456789012345678901234");

            var empresa = new Empresa("Empresa Teste", "Empresa", new Cnpj("12345678000199"), "0000000000",
            "Rua teste", "1", "Bairro do teste", "0000", "São Paulo", "SP", "0000000", "1055", "Brasil");

            var nota = new NotaFiscal(chave, 1, 1, DateTime.Now, empresa, new ValorMonetario(100));

            nota.AdicionarItem(new ItemNotaFiscal(nota.Id, 1, "00001", "Produto Teste", 1, new ValorMonetario(100), new ValorMonetario(100)));


            var validator = new TotalItensValidator();
            // Act
            var resultado = validator.Validar(nota);    
            // Assert
            Assert.Null(resultado);
        }
    }
}
