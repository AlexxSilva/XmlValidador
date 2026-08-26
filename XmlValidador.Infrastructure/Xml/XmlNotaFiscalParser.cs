using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;
using XmlValidador.Domain.Exceptions;
using XmlValidador.Domain.ValueObjects;

namespace XmlValidador.Infrastructure.Xml
{
    public class XmlNotaFiscalParser : IXmlNotaFiscalParser
    {
        public NotaFiscal Parse(string xml)
        {

            if (string.IsNullOrWhiteSpace(xml))
                throw new XmlInvalidoException("O XML não pode ser vazio.");

            try
            {
                var documento = XDocument.Parse(xml);

                XNamespace ns = "http://www.portalfiscal.inf.br/nfe";

                var infNFe = documento
                    .Descendants(ns + "infNFe")
                    .FirstOrDefault();

                if (infNFe == null)
                    throw new XmlInvalidoException(
                        "O XML não possui o elemento infNFe.");

                var identificacao = ExtrairIdentificacao(ns, infNFe);

                var chaveAcesso = ExtrairChave(infNFe);

                var empresa = ExtrairEmpresa(infNFe, ns);

                ValorMonetario valorTotalNota = ExtrairTotalNota(ns, infNFe);

                var notaFiscal = new NotaFiscal(
                    chaveAcesso,
                    identificacao.Numero,
                    identificacao.Serie,
                    identificacao.DataEmissao,
                    empresa,
                    valorTotalNota);

                AdicionarItens(ns, infNFe, notaFiscal);
                return notaFiscal;
            }
            catch (XmlException)
            {
                throw new XmlInvalidoException(
                    "O XML possui uma estrutura inválida.");
            }
        }

        private static IdentificacaoNfe ExtrairIdentificacao(XNamespace ns, XElement infNFe)
        {
            var ide = infNFe.Element(ns + "ide");

            if (ide == null)
                throw new XmlInvalidoException(
                    "Elemento ide não encontrado.");

            var dataEmissaoTexto = ide.Element(ns + "dhEmi")?.Value;

            if (!DateTime.TryParse(dataEmissaoTexto, out var dataEmissao))
            {
                throw new XmlInvalidoException(
                    "Data de emissão inválida.");
            }

            var numeroNotaTexto = ide.Element(ns + "nNF")?.Value;

            if (!int.TryParse(numeroNotaTexto,out var numeroNota))
            {
                throw new XmlInvalidoException(
                    "Número da NF-e inválido.");
            }

            var serieTexto = ide.Element(ns + "serie")?.Value;

            if (!int.TryParse(serieTexto,out var serie))
            {
                throw new XmlInvalidoException(
                    "Série da NF-e inválida.");
            }

            return new IdentificacaoNfe(dataEmissao,numeroNota, serie);
        }

        private static ChaveAcessoNfe ExtrairChave(XElement infNFe)
        {
            var chave = infNFe
                .Attribute("Id")?
                .Value;

            if (string.IsNullOrWhiteSpace(chave))
                throw new XmlInvalidoException(
                    "Chave de acesso não encontrada.");

            if (chave.StartsWith("NFe"))
                chave = chave.Substring(3);

            try
            {
                return new ChaveAcessoNfe(chave);
            }
            catch (ArgumentException ex)
            {
                throw new XmlInvalidoException(
                    $"Chave de acesso inválida: {ex.Message}");
            }
        }

        private static Empresa ExtrairEmpresa(XElement infNFe, XNamespace ns)
        {

            var emit = infNFe.Element(ns + "emit");

            if (emit == null)
                throw new XmlInvalidoException(
                    "Emitente não encontrado.");

            var razaoSocial = emit
                .Element(ns + "xNome")?
                .Value;

            var nomeFantasia = emit
                .Element(ns + "xFant")?
                .Value;

            var cnpj = emit
                .Element(ns + "CNPJ")?
                .Value;

            var inscricaoEstadual = emit
                .Element(ns + "IE")?
                .Value;

            if (string.IsNullOrWhiteSpace(cnpj))
                throw new XmlInvalidoException(
                    "CNPJ do emitente não encontrado.");

            if (string.IsNullOrWhiteSpace(razaoSocial))
                throw new XmlInvalidoException(
                    "Nome do emitente não encontrado.");

            var endereco = emit.Element(ns + "enderEmit");

            if (endereco == null)
                throw new XmlInvalidoException(
                    "Endereço do emitente não encontrado.");

            var logradouro = endereco.Element(ns + "xLgr")?.Value;
            var nro = endereco.Element(ns + "nro")?.Value;
            var bairro = endereco.Element(ns + "xBairro")?.Value;
            var cMunicipio = endereco.Element(ns + "cMun")?.Value;
            var municipio = endereco.Element(ns + "xMun")?.Value;
            var uf = endereco.Element(ns + "UF")?.Value;
            var cep = endereco.Element(ns + "CEP")?.Value;
            var cPais = endereco.Element(ns + "cPais")?.Value;
            var pais = endereco.Element(ns + "xPais")?.Value;

            try
            {
                var cnpjFormat = new Cnpj(cnpj);

                var empresa = new Empresa(
                    razaoSocial,
                    nomeFantasia,
                    cnpjFormat,
                    inscricaoEstadual,
                    logradouro,
                    nro,
                    bairro,
                    cMunicipio,
                    municipio,
                    uf,
                    cep,
                    cPais,
                    pais);

                return empresa;
            }
            catch (ArgumentException ex)
            {
                throw new XmlInvalidoException(
                    $"CNPJ do emitente inválido: {ex.Message}");
            }
        }

        private static ValorMonetario ExtrairTotalNota(XNamespace ns,XElement infNFe)
        {
            var icmsTot = infNFe
                .Element(ns + "total")?
                .Element(ns + "ICMSTot");

            if (icmsTot == null)
                throw new XmlInvalidoException(
                    "Totais da NF-e não encontrados.");

            var valorTotalNotaTexto = icmsTot
                .Element(ns + "vNF")?
                .Value;

            if (!decimal.TryParse(
                    valorTotalNotaTexto,
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out var valorTotalNota))
            {
                throw new XmlInvalidoException(
                    "Valor total da NF-e inválido.");
            }

            try
            {
                return new ValorMonetario(valorTotalNota);
            }
            catch (ArgumentException ex)
            {
                throw new XmlInvalidoException(
                    $"Valor total da NF-e inválido: {ex.Message}");
            }
        }

        private static void AdicionarItens(XNamespace ns, XElement infNFe, NotaFiscal notaFiscal)
        {
            foreach (var det in infNFe.Elements(ns + "det"))
            {
                var prod = det.Element(ns + "prod");

                if (prod == null)
                    continue;

                var numeroItemTexto = det
                    .Attribute("nItem")?
                    .Value;

                if (!int.TryParse(
                        numeroItemTexto,
                        out var numeroItem))
                {
                    throw new XmlInvalidoException(
                        "Número do item inválido.");
                }

                var codigo = prod
                    .Element(ns + "cProd")?
                    .Value;

                var descricao = prod
                    .Element(ns + "xProd")?
                    .Value;

                var quantidadeTexto = prod
                    .Element(ns + "qCom")?
                    .Value;

                var valorUnitarioTexto = prod
                    .Element(ns + "vUnCom")?
                    .Value;

                var valorTotalTexto = prod
                    .Element(ns + "vProd")?
                    .Value;

                if (!decimal.TryParse(
                        quantidadeTexto,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out var quantidade))
                {
                    throw new XmlInvalidoException(
                        $"Quantidade inválida no item {numeroItem}.");
                }

                if (!decimal.TryParse(
                        valorUnitarioTexto,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out var valorUnitario))
                {
                    throw new XmlInvalidoException(
                        $"Valor unitário inválido no item {numeroItem}.");
                }

                if (!decimal.TryParse(
                        valorTotalTexto,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out var valorTotal))
                {
                    throw new XmlInvalidoException(
                        $"Valor total inválido no item {numeroItem}.");
                }

                try
                {
                    var valorUnitarioFormat =
                        new ValorMonetario(valorUnitario);

                    var valorTotalFormat =
                        new ValorMonetario(valorTotal);

                    var item = new ItemNotaFiscal(
                        notaFiscal.Id,
                        numeroItem,
                        codigo,
                        descricao,
                        quantidade,
                        valorUnitarioFormat,
                        valorTotalFormat);

                    notaFiscal.AdicionarItem(item);
                }
                catch (ArgumentException ex)
                {
                    throw new XmlInvalidoException(
                        $"Dados inválidos no item {numeroItem}: {ex.Message}");
                }
            }
        }

        private record IdentificacaoNfe(DateTime DataEmissao, int Numero, int Serie);
    }
}
