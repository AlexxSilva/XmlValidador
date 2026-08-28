using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Application.DTOs;

namespace XmlValidador.Application.UseCases.ImportarXml
{
    public interface IImportarXmlUseCase
    {
        Task<ResultadoValidacaoDto> Executar(string xml);
    }
}
