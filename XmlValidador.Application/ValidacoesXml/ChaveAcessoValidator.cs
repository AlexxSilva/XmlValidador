using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Application.ValidacoesXml
{
    public class ChaveAcessoValidator : IRegraValidacao
    {
        public string Codigo => "NFE_SEM_CHAVE_ACESSO";
        public string? Validar(NotaFiscal notaFiscal)
        {

            if (notaFiscal.ChaveAcesso == null)
            {
                return "Chave de acesso não informada.";
            }

            return null;
        }
    }
}
