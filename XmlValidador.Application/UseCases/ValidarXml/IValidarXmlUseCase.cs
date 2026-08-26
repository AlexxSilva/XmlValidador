using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.DTOs;

namespace XmlValidador.Application.UseCases.ValidarXml
{
    public interface IValidarXmlUseCase
    {
        Task<ResultadoValidacaoDto> Executar(string xml);
    }
}
