using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Application.ValidacoesXml
{
    public class CnpjEmitenteValidator : IRegraValidacao
    {
        public string Codigo => "NFE_SEM_CNPJ_EMITENTE";

        public string? Validar(NotaFiscal notaFiscal)
        {

            if (notaFiscal.Empresa?.Cnpj == null)
            {
                return "CNPJ do emitente não informado.";
            }

            return null;
        }
    }
}
