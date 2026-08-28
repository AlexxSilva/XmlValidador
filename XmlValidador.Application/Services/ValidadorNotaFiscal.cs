using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.DTOs;
using XmlValidador.Application.Interfaces;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Application.Services
{
    public class ValidadorNotaFiscal : IValidadorNotaFiscal
    {
        private readonly IEnumerable<IRegraValidacao> _regras;

        public ValidadorNotaFiscal(
            IEnumerable<IRegraValidacao> regras)
        {
            _regras = regras;
        }

        public ResultadoValidacaoDto Validar(NotaFiscal notaFiscal)
        {
            var resultado = new ResultadoValidacaoDto();

            foreach (var regra in _regras)
            {
                var erro = regra.Validar(notaFiscal);

                if (erro != null)
                {
                    resultado.Erros.Add(
                        new ErroValidacaoDto(
                            regra.Codigo,
                            erro));
                }
            }

            resultado.Valido = !resultado.Erros.Any();

            return resultado;
        }
    }
}
