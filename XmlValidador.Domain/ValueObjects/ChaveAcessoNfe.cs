using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Domain.ValueObjects
{
    public sealed class ChaveAcessoNfe
    {
        public string Valor { get; }

        public ChaveAcessoNfe(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("Chave de acesso é obrigatória.");

            if(valor.Length != 44)
                throw new ArgumentException("Quantidade de caracteres inválida.");

            Valor = valor;
        }
    }
}
