using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;
using XmlValidador.Application.UseCases.ValidarXml;
using XmlValidador.Application.ValidacoesXml;
using XmlValidador.Infrastructure.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//versão tradicional swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery();

builder.Services.AddScoped<IXmlNotaFiscalParser, XmlNotaFiscalParser>();
builder.Services.AddScoped<IValidarXmlUseCase, ValidarXmlUseCase>();
builder.Services.AddScoped<IRegraValidacao, NfePossuiItensValidator>();
builder.Services.AddScoped<IRegraValidacao, CnpjEmitenteValidator>();
builder.Services.AddScoped<IRegraValidacao, NumeroNfeValidator>();
builder.Services.AddScoped<IRegraValidacao, ChaveAcessoValidator>();
builder.Services.AddScoped<IRegraValidacao, TotalItensValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
    //app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAntiforgery();


app.MapPost("/validar-xml", async (IFormFile arquivo, IValidarXmlUseCase validarXml) =>
{
    using var reader = new StreamReader(arquivo.OpenReadStream());
    var xml = await reader.ReadToEndAsync();
    var resultado = validarXml.Executar(xml);
    return Results.Ok(resultado);
}).DisableAntiforgery();

app.Run();

