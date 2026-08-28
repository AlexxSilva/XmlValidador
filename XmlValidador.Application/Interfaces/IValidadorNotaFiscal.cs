using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.DTOs;
using XmlValidador.Domain.Entities;

namespace XmlValidador.Application.Interfaces
{
    public interface IValidadorNotaFiscal
    {
        ResultadoValidacaoDto Validar(NotaFiscal notaFiscal);
    }
}
