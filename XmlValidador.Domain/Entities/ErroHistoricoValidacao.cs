using System;
using System.Collections.Generic;
using System.Text;
using XmlValidador.Domain.Enums;

namespace XmlValidador.Domain.Entities
{
    public class ErroHistoricoValidacao
    {
        public Guid Id { get; private set; }

        public Guid HistoricoValidacaoId { get; private set; }

        public HistoricoValidacao HistoricoValidacao { get; private set; }

        public string Codigo { get; private set; }

        public string Mensagem { get; private set; }

        public SeveridadeValidacao Severidade { get; private set; }

        private ErroHistoricoValidacao()
        {
        }

        public ErroHistoricoValidacao(
            Guid historicoValidacaoId,
            string codigo,
            string mensagem,
            SeveridadeValidacao severidade)
        {
            Id = Guid.NewGuid();

            HistoricoValidacaoId = historicoValidacaoId;

            Codigo = codigo;

            Mensagem = mensagem;

            Severidade = severidade;
        }
    }
}
