using XmlValidador.Application.Interfaces;
using XmlValidador.Application.Services;
using XmlValidador.Application.UseCases.ValidarXml;
using XmlValidador.Application.ValidacoesXml;
using XmlValidador.Domain.Entities;
using XmlValidador.Domain.Exceptions;
using XmlValidador.Domain.ValueObjects;

public class ValidarXmlUseCaseTests
{
    [Fact]
    public async Task DeveRetornarErroQuandoXmlForInvalido()
    {
        // Arrange

        var parser = new ParserFakeXmlInvalido();

        var regras = new List<IRegraValidacao>();

        var validador = new ValidadorNotaFiscal(regras);

        var useCase = new ValidarXmlUseCase(
            parser,
            validador);

        // Act

        var resultado = await useCase.Executar(
            "qualquer coisa");

        // Assert

        Assert.False(resultado.Valido);

        Assert.Contains(
            resultado.Erros,
            erro => erro.Codigo == "XML_INVALIDO" &&
                    erro.Mensagem ==
                    "O XML possui uma estrutura inválida.");
    }

    [Fact]
    public async Task DeveRetornarValidoQuandoXmlForValido()
    {
        // Arrange

        var notaFiscal = CriarNotaFiscalValida();

        var parser = new ParserFakeXmlValido(notaFiscal);

        var regras = new List<IRegraValidacao>();

        var validador = new ValidadorNotaFiscal(regras);

        var useCase = new ValidarXmlUseCase(
            parser,
            validador);

        // Act

        var resultado = await useCase.Executar(
            "xml válido");

        // Assert

        Assert.True(resultado.Valido);

        Assert.Empty(resultado.Erros);
    }

    [Fact]
    public async Task DeveRetornarErroQuandoTotalDosItensForDiferenteDoTotalDaNota()
    {
        // Arrange

        var notaFiscal = CriarNotaFiscalComTotalInvalido();

        var parser = new ParserFakeXmlValido(notaFiscal);

        var regras = new List<IRegraValidacao>
            {
                new TotalItensValidator()
            };

        var validador = new ValidadorNotaFiscal(regras);

        var useCase = new ValidarXmlUseCase(
            parser,
            validador);

        // Act

        var resultado = await useCase.Executar(
            "xml válido");

        // Assert

        Assert.False(resultado.Valido);

        Assert.Contains(
            resultado.Erros,
            erro => erro.Codigo ==
                "NFE_VALOR_TOTAL_ITENS_INCORRETO" &&
                erro.Mensagem.Contains("soma dos itens"));
    }

    [Fact]
    public void DeveRetornarErroQuandoNcmNaoTiver8Digitos()
    {
        // Arrange
        var notaFiscal = CriarNotaFiscalValida();

        var item = new ItemNotaFiscal(
        notaFiscal.Id,
        1,
        "123",
        "Produto teste",
        "1234567",
        1,
        new ValorMonetario(100),
        new ValorMonetario(100));

        notaFiscal.AdicionarItem(item);

        var validator = new NcmValidator();

        var resultado = validator.Validar(notaFiscal);

        Assert.NotNull(resultado);
    }
    [Fact]
    public void DeveRetornarValidoQuandoNcmTiver8Digitos()
    {
        // Arrange
        var notaFiscal = CriarNotaFiscalValida();

        var item = new ItemNotaFiscal(
        notaFiscal.Id,
        1,
        "123",
        "Produto teste",
        "12345678",
        1,
        new ValorMonetario(100),
        new ValorMonetario(100));

        notaFiscal.AdicionarItem(item);

        var validator = new NcmValidator();

        var resultado = validator.Validar(notaFiscal);

        Assert.Null(resultado);
    }

    [Fact]
    public void DeveRetornarErroQuandoCodigoIbgeNaoTiver7Digitos() 
    {
        //Arrange
        var notafiscal = CriarNotaFiscalComEmpresaComIbgeInvalido();
        var validador = new CodigoIbgeMunicipioValidator();

        //Act
        var resultado = validador.Validar(notafiscal);

        Assert.NotNull(resultado);
    }

    [Fact]
    public void DeveRetornarValidoQuandoCodigoIbgeTiver7Digitos()
    {
        //Arrange
        var notafiscal = CriarNotaFiscalValida();
        var validador = new CodigoIbgeMunicipioValidator();

        //Act
        var resultado = validador.Validar(notafiscal);

        Assert.Null(resultado);
    }


    private Empresa CriarEmpresa()
    {
        return new Empresa(
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
    }
    private Empresa CriarEmpresaIbgeInvalido()
    {
        return new Empresa(
            "Empresa Teste",
            "Empresa Teste",
            new Cnpj("12345678000195"),
            "123456789",
            "Rua Teste",
            "100",
            "Centro",
            "1234",
            "São Paulo",
            "SP",
            "01000000",
            "1058",
            "Brasil"
        );
    }

    private NotaFiscal CriarNotaFiscalValida()
    {
        var empresa = CriarEmpresa();

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

    private NotaFiscal CriarNotaFiscalComEmpresaComIbgeInvalido()
    {
        var empresa = CriarEmpresaIbgeInvalido();

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

    private NotaFiscal CriarNotaFiscalComTotalInvalido()
    {
        var empresa = CriarEmpresa();

        var notaFiscal = new NotaFiscal(
            new ChaveAcessoNfe(
                "35260812345678000195550010000000011000000010"),
            1,
            1,
            DateTime.Now,
            empresa,
            new ValorMonetario(200m)
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


public class ParserFakeXmlInvalido : IXmlNotaFiscalParser
{
    public NotaFiscal Parse(string xml)
    {
        throw new XmlInvalidoException(
            "O XML possui uma estrutura inválida.");
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