using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Domain.ValueObjects
{
    public sealed class Cnpj
    {
        public string Valor { get; }

        public Cnpj(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("CNPJ é obrigatório.");

            if (valor.Length != 14)
                throw new ArgumentException("Quantidade de caracteres inválida.");

            Valor = valor;
        }
    }
}
