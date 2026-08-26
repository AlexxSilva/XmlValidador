using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Application.ValidacoesXml
{
    public class NfePossuiItensValidator : IRegraValidacao
    {
        public string Codigo => "NFE_SEM_ITENS";
        public string? Validar(NotaFiscal notaFiscal)
        {
            if (!notaFiscal.Itens.Any())
            {
                return "A NF-e não possui itens.";
            }

            return null;
        }
    }
}
