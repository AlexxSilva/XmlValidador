using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Security.Cryptography;

namespace XmlValidador.Domain.Entities;

public class Empresa
{
    public int Id { get; set; }
    public string? RazaoSocial { get; set; }
    public string? NomeFantasia { get; set; }
    public string? Cnpj { get; set; }
    public string? InscricaoEstadual { get; set; }
    public string ? Lgr { get; set; }
    public string ? Nro { get; set; }
    public string? Bairro { get; set; }
    public string? CMun { get; set; }
    public string? Mun { get; set; }
    public string? UF { get; set; }
    public string? CEP { get; set; }
    public string? CPais { get; set; }
    public string? Pais { get; set; }  
}
