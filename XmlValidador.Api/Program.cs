using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;
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


app.MapPost("/teste-parser", async (
    IFormFile arquivo,
    IXmlNotaFiscalParser parser) =>
{
    using var reader = new StreamReader(arquivo.OpenReadStream());

    var xml = await reader.ReadToEndAsync();

    var notaFiscal = parser.Parse(xml);

    return Results.Ok(notaFiscal);
})
.DisableAntiforgery();

app.Run();

