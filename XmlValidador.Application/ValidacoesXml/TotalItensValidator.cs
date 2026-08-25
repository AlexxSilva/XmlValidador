using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Application.ValidacoesXml
{
    public class TotalItensValidator : IRegraValidacao
    {
        public string? Validar(NotaFiscal notaFiscal)
        {
            if (notaFiscal.Itens.Any())
            {
                var somaItens = notaFiscal.Itens
                    .Sum(x => x.ValorTotal.Valor);

                if (somaItens != notaFiscal.ValorTotal.Valor)
                {
                    return
                        $"A soma dos itens ({somaItens:F2}) " +
                        $"não corresponde ao valor total da NF-e " +
                        $"({notaFiscal.ValorTotal.Valor:F2}).";
                }
            }

            return null;
        }
    }
}
