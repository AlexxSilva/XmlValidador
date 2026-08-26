using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Security.Cryptography;
using XmlValidador.Domain.ValueObjects;

namespace XmlValidador.Domain.Entities;

public class Empresa
{
    public Guid Id { get; private set; }
    public string RazaoSocial { get; private set; }
    public string? NomeFantasia { get; private set; }
    public Cnpj Cnpj { get; private set; }
    public string? InscricaoEstadual { get; private set; }
    public string ? Lgr { get; private set; }
    public string ? Nro { get; private set; }
    public string? Bairro { get; private set; }
    public string? CMun { get; private set; }
    public string? Mun { get; private set; }
    public string? UF { get; private set; }
    public string? CEP { get; private set; }
    public string? CPais { get; private set; }
    public string? Pais { get; private set; }
    public DateTime DataCadastro { get; private set; }


    private Empresa()
    {
    }

    public Empresa(string razaoSocial, string? nomeFantasia, Cnpj cnpj, 
        string? inscricaoEstadual, string? lgr, string? nro, string? bairro, 
        string? cMun, string? mun, string? uF, string? cEP, string? cPais, string? pais)
    {
        Id = Guid.NewGuid();
        RazaoSocial = razaoSocial;
        NomeFantasia = nomeFantasia;
        Cnpj = cnpj;
        InscricaoEstadual = inscricaoEstadual;
        Lgr = lgr;
        Nro = nro;
        Bairro = bairro;
        CMun = cMun;
        Mun = mun;
        UF = uF;
        CEP = cEP;
        CPais = cPais;
        Pais = pais;
        DataCadastro = DateTime.Now;
    }

}
