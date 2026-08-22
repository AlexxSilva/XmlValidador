using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Domain.Entities
{
    public class ValidacaoNotaFiscal
    {

        public Guid Id { get; private set; }

        public int NotaFiscalId { get; private set; }
        public int Codigo { get; private set; }
        public int Descricao { get; private set; }
        public int Severidade { get; private set; }
        public int Status { get; private set; }
        public int Evidencia { get; private set; }

        public DateTime DataCadastro { get; private set; }


        public ValidacaoNotaFiscal(int notaFiscalId, int codigo, int descricao, 
            int severidade, int status, int evidencia)
        {
            Id = new Guid();
            NotaFiscalId = notaFiscalId;
            Codigo = codigo;
            Descricao = descricao;
            Severidade = severidade;
            Status = status;
            Evidencia = evidencia;
            DataCadastro = DateTime.Now;
        }
    }
}
