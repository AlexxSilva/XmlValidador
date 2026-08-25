using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Application.ValidacoesXml
{
    public class NumeroNfeValidator : IRegraValidacao
    {
        public string? Validar(NotaFiscal notaFiscal)
        {
            if (notaFiscal.Numero <= 0)
            {
                return ("Número da NF - e inválido.");
            }

            return null;
        }
    }
}
