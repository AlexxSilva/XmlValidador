using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Application.ValidacoesXml
{
    public class NcmValidator : IRegraValidacao
    {
        public string Codigo => "NCM_INVALIDO";

        public string? Validar(NotaFiscal notaFiscal)
        {
            foreach(var item in notaFiscal.Itens)
            {
                if (string.IsNullOrWhiteSpace(item.Ncm))
                {
                    return $"O NCM do item {item.Nitem} deve ser informado.";
                }

                if (item.Ncm.Length != 8 || !item.Ncm.All(char.IsDigit))
                {
                    return $"O NCM do item {item.Nitem} deve possuir 8 dígitos numéricos.";
                }
            }
            return null;
        }
    }
}
