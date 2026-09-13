using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;
using XmlValidador.Application.Services;
using XmlValidador.Application.UseCases.ImportarXml;
using XmlValidador.Application.UseCases.ValidarXml;
using XmlValidador.Application.ValidacoesXml;
using XmlValidador.Domain.Entities;
using XmlValidador.Domain.ValueObjects;
using XmlValidador.Infrastructure.Data;
using XmlValidador.Infrastructure.Repositories;
using XmlValidador.Infrastructure.Xml;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

//ConexaoBancoDados
builder.Services.AddDbContext<XmlValidadorDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//versão tradicional swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAntiforgery();




builder.Services.AddScoped<IXmlNotaFiscalParser, XmlNotaFiscalParser>();
builder.Services.AddScoped<IValidarXmlUseCase, ValidarXmlUseCase>();
builder.Services.AddScoped<IRegraValidacao, NfePossuiItensValidator>();
builder.Services.AddScoped<IRegraValidacao, CnpjEmitenteValidator>();
builder.Services.AddScoped<IRegraValidacao, ChaveAcessoValidator>();
builder.Services.AddScoped<IRegraValidacao, TotalItensValidator>();
builder.Services.AddScoped<IRegraValidacao, NcmValidator>();
builder.Services.AddScoped<IRegraValidacao, CodigoIbgeMunicipioValidator>();
builder.Services.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
builder.Services.AddScoped<IValidadorNotaFiscal, ValidadorNotaFiscal>();
builder.Services.AddScoped<IImportarXmlUseCase,ImportarXmlUseCase>();
builder.Services.AddScoped<IHistoricoValidacaoRepository, HistoricoValidacaoRepository>();



builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
}); //Converter enum para string no JSON

builder.Services.AddCors(options =>
{
    options.AddPolicy("Blazor", policy =>
    {
        policy
            .WithOrigins(
                "https://localhost:7299",
                "http://localhost:5023")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("Blazor");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
    //app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAntiforgery();


app.MapPost("/api/nfe/validar",
    async (
        IFormFile arquivo,
        IValidarXmlUseCase validarXml) =>
    {
        using var reader = new StreamReader(
            arquivo.OpenReadStream());

        var xml = await reader.ReadToEndAsync();

        var resultado = await validarXml.Executar(xml);

        return Results.Ok(resultado);
    })
    .DisableAntiforgery();


app.MapPost("/api/nfe/importar",
    async (
        IFormFile arquivo,
        IImportarXmlUseCase importarXml) =>
    {
        using var reader = new StreamReader(
            arquivo.OpenReadStream());

        var xml = await reader.ReadToEndAsync();

        var resultado = await importarXml.Executar(xml);

        return Results.Ok(resultado);
    })
    .DisableAntiforgery();



app.Run();

