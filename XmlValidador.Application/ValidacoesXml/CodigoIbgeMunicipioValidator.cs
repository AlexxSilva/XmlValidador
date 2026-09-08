    using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Application.ValidacoesXml
{
    public class CodigoIbgeMunicipioValidator : IRegraValidacao
    {
        public string Codigo => "IBGE_MUNICIPIO_INVALIDO";

        public string? Validar(NotaFiscal notaFiscal)
        {
            var codigoIbge = notaFiscal.Empresa.CMun;

            if (string.IsNullOrWhiteSpace(codigoIbge))
                return "O código IBGE do município deve ser informado.";

            if (codigoIbge.Length != 7 || !codigoIbge.All(char.IsDigit))
                return "O código IBGE do município deve possuir 7 dígitos numéricos.";

            return null;
        }
    }
}
