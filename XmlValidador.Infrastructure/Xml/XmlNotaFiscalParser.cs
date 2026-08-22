using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;
using XmlValidador.Domain.ValueObjects;

namespace XmlValidador.Infrastructure.Xml
{
    public class XmlNotaFiscalParser : IXmlNotaFiscalParser
    {
        public NotaFiscal Parse(string xml)
        {
            //if (string.IsNullOrWhiteSpace(xml))
            //    throw new ArgumentException("O XML não pode ser vazio.");

            //var documento = XDocument.Parse(xml);

            //XNamespace ns = "http://www.portalfiscal.inf.br/nfe";

            //var infNFe = documento
            //    .Descendants(ns + "infNFe")
            //    .FirstOrDefault();

            //if (infNFe == null)
            //    throw new InvalidOperationException(
            //        "O XML não possui o elemento infNFe.");

            //// =========================
            //// CHAVE DE ACESSO
            //// =========================

            //var chave = infNFe
            //    .Attribute("Id")?
            //    .Value;

            //if (string.IsNullOrWhiteSpace(chave))
            //    throw new InvalidOperationException(
            //        "Chave de acesso não encontrada.");

            //chave = chave.Replace("NFe", "");

            //var chaveAcesso = new ChaveAcessoNfe(chave);

            //// =========================
            //// EMITENTE
            //// =========================

            //var emit = infNFe.Element(ns + "emit");

            //if (emit == null)
            //    throw new InvalidOperationException(
            //        "Emitente não encontrado.");

            //var cnpjTexto = emit
            //    .Element(ns + "CNPJ")?
            //    .Value;

            //var nome = emit
            //    .Element(ns + "xNome")?
            //    .Value;

            //if (string.IsNullOrWhiteSpace(cnpjTexto))
            //    throw new InvalidOperationException(
            //        "CNPJ do emitente não encontrado.");

            //if (string.IsNullOrWhiteSpace(nome))
            //    throw new InvalidOperationException(
            //        "Nome do emitente não encontrado.");

            //var cnpj = new Cnpj(cnpjTexto);

            //var empresa = new Empresa(
            //    cnpj,
            //    nome
            //);

            //// =========================
            //// DATA DE EMISSÃO
            //// =========================

            //var ide = infNFe.Element(ns + "ide");

            //var dataEmissaoTexto = ide?
            //    .Element(ns + "dhEmi")?
            //    .Value;

            //if (!DateTime.TryParse(dataEmissaoTexto, out var dataEmissao))
            //    throw new InvalidOperationException(
            //        "Data de emissão inválida.");

            //// =========================
            //// ITENS
            //// =========================

            //var itens = new List<ItemNotaFiscal>();

            //foreach (var det in infNFe.Elements(ns + "det"))
            //{
            //    var prod = det.Element(ns + "prod");

            //    if (prod == null)
            //        continue;

            //    var codigo = prod
            //        .Element(ns + "cProd")?
            //        .Value;

            //    var descricao = prod
            //        .Element(ns + "xProd")?
            //        .Value;

            //    var quantidadeTexto = prod
            //        .Element(ns + "qCom")?
            //        .Value;

            //    var valorUnitarioTexto = prod
            //        .Element(ns + "vUnCom")?
            //        .Value;

            //    var valorTotalTexto = prod
            //        .Element(ns + "vProd")?
            //        .Value;

            //    if (!decimal.TryParse(
            //            quantidadeTexto,
            //            System.Globalization.NumberStyles.Any,
            //            System.Globalization.CultureInfo.InvariantCulture,
            //            out var quantidade))
            //    {
            //        throw new InvalidOperationException(
            //            $"Quantidade inválida no item {codigo}.");
            //    }

            //    if (!decimal.TryParse(
            //            valorUnitarioTexto,
            //            System.Globalization.NumberStyles.Any,
            //            System.Globalization.CultureInfo.InvariantCulture,
            //            out var valorUnitario))
            //    {
            //        throw new InvalidOperationException(
            //            $"Valor unitário inválido no item {codigo}.");
            //    }

            //    if (!decimal.TryParse(
            //            valorTotalTexto,
            //            System.Globalization.NumberStyles.Any,
            //            System.Globalization.CultureInfo.InvariantCulture,
            //            out var valorTotal))
            //    {
            //        throw new InvalidOperationException(
            //            $"Valor total inválido no item {codigo}.");
            //    }

            //    var item = new ItemNotaFiscal(
            //        codigo,
            //        descricao,
            //        quantidade,
            //        valorUnitario,
            //        valorTotal
            //    );

            //    itens.Add(item);
            //}

            //// =========================
            //// TOTAL DA NOTA
            //// =========================

            //var icmsTot = infNFe
            //    .Element(ns + "total")?
            //    .Element(ns + "ICMSTot");

            //var valorTotalNotaTexto = icmsTot?
            //    .Element(ns + "vNF")?
            //    .Value;

            //if (!decimal.TryParse(
            //        valorTotalNotaTexto,
            //        System.Globalization.NumberStyles.Any,
            //        System.Globalization.CultureInfo.InvariantCulture,
            //        out var valorTotalNota))
            //{
            //    throw new InvalidOperationException(
            //        "Valor total da NF-e inválido.");
            //}

            //var valorMonetario = new ValorMonetario(
            //    valorTotalNota
            //);

            //// =========================
            //// NOTA FISCAL
            //// =========================

            //var notaFiscal = new NotaFiscal(
            //    chaveAcesso,
            //    empresa,
            //    dataEmissao,
            //    valorMonetario,
            //    itens
            //);

            //return notaFiscal;
            return null;
        }
    }
}
