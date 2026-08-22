using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Domain.ValueObjects
{
    public sealed class ValorMonetario
    {
        public decimal Valor { get; }

        public ValorMonetario(decimal valor)
        {
            if (valor < 0)
                throw new ArgumentException("O valor não pode ser negativo.");

            Valor = valor;
        }
    }
}
