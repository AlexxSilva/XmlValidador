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
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentException("O XML não pode ser vazio.");

            var documento = XDocument.Parse(xml);

            XNamespace ns = "http://www.portalfiscal.inf.br/nfe";

            var infNFe = documento
                .Descendants(ns + "infNFe")
                .FirstOrDefault();

            if (infNFe == null)
                throw new InvalidOperationException(
                    "O XML não possui o elemento infNFe.");

            DateTime dataEmissao;
            int numeroNota = 0;
            int serie = 0;

            ExtrairIdentificacao(ns, infNFe, out dataEmissao, out numeroNota, out serie);

            var chaveAcesso = ExtrairChave(infNFe);

            var empresa = ExtrairEmpresa(infNFe, ns);

            decimal valorTotalNota = ExtrairTotalNota(ns, infNFe);

            var valorMonetario =
                new ValorMonetario(valorTotalNota);

            // =========================
            // CRIA NOTA FISCAL
            // =========================

            var notaFiscal = new NotaFiscal(
                chaveAcesso,
                numeroNota,
                serie,
                dataEmissao,
                empresa,
                valorMonetario);

            // =========================
            // ITENS
            // =========================

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
                    throw new InvalidOperationException(
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
                    throw new InvalidOperationException(
                        $"Quantidade inválida no item {numeroItem}.");
                }

                if (!decimal.TryParse(
                        valorUnitarioTexto,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out var valorUnitario))
                {
                    throw new InvalidOperationException(
                        $"Valor unitário inválido no item {numeroItem}.");
                }

                if (!decimal.TryParse(
                        valorTotalTexto,
                        System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture,
                        out var valorTotal))
                {
                    throw new InvalidOperationException(
                        $"Valor total inválido no item {numeroItem}.");
                }

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

            // =========================
            // RETORNO
            // =========================

            return notaFiscal;
        }

        private static decimal ExtrairTotalNota(XNamespace ns, XElement infNFe)
        {
            // =========================
            // TOTAL DA NOTA
            // =========================

            var icmsTot = infNFe
                .Element(ns + "total")?
                .Element(ns + "ICMSTot");

            if (icmsTot == null)
                throw new InvalidOperationException(
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
                throw new InvalidOperationException(
                    "Valor total da NF-e inválido.");
            }

            return valorTotalNota;
        }

        private static void ExtrairIdentificacao(XNamespace ns, XElement infNFe, 
            out DateTime dataEmissao, out int numeroNota, out int serie)
        {
            var ide = infNFe.Element(ns + "ide");

            if (ide == null)
                throw new InvalidOperationException(
                    "Elemento ide não encontrado.");

            var dataEmissaoTexto = ide
                .Element(ns + "dhEmi")?
                .Value;

            if (!DateTime.TryParse(
                    dataEmissaoTexto,
                    out dataEmissao))
            {
                throw new InvalidOperationException(
                    "Data de emissão inválida.");
            }

            var numeroNotaTexto = ide
                .Element(ns + "nNF")?
                .Value;

            if (!int.TryParse(
                    numeroNotaTexto,
                    out numeroNota))
            {
                throw new InvalidOperationException(
                    "Número da NF-e inválido.");
            }

            var serieTexto = ide
                .Element(ns + "serie")?
                .Value;

            if (!int.TryParse(
                    serieTexto,
                    out serie))
            {
                throw new InvalidOperationException(
                    "Série da NF-e inválida.");
            }
        }

        private ChaveAcessoNfe ExtrairChave(XElement infNFe)
        {
            var chave = infNFe
                .Attribute("Id")?
                .Value;

            if (string.IsNullOrWhiteSpace(chave))
                throw new InvalidOperationException(
                    "Chave de acesso não encontrada.");

            if (chave.StartsWith("NFe"))
                chave = chave.Substring(3);

            var chaveAcesso = new ChaveAcessoNfe(chave);

            return chaveAcesso;
        }

        private Empresa ExtrairEmpresa(XElement infNFe, XNamespace ns)
        {

            var emit = infNFe.Element(ns + "emit");

            if (emit == null)
                throw new InvalidOperationException(
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
                throw new InvalidOperationException(
                    "CNPJ do emitente não encontrado.");

            if (string.IsNullOrWhiteSpace(razaoSocial))
                throw new InvalidOperationException(
                    "Nome do emitente não encontrado.");

            var endereco = emit.Element(ns + "enderEmit");

            if (endereco == null)
                throw new InvalidOperationException(
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

            var cnpjFormat = new Cnpj(cnpj);

            var empresa = new Empresa(razaoSocial,nomeFantasia,cnpjFormat,inscricaoEstadual,
            logradouro,nro,bairro,cMunicipio,municipio,uf,cep,cPais,pais);

            return empresa;
        }
        

    }
}
