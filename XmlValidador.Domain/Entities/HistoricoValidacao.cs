using System;
using System.Collections.Generic;
using System.Text;

namespace XmlValidador.Domain.Entities
{
    public class HistoricoValidacao
    {
        public Guid Id { get; private set; }

        public Guid? NotaFiscalId { get; private set; }
        public NotaFiscal? NotaFiscal { get; private set; }

        public DateTime DataValidacao { get; private set; }

        public bool Valido { get; private set; }

        public string Xml { get; private set; }

        private readonly List<ErroHistoricoValidacao> _erros = new();

        public IReadOnlyCollection<ErroHistoricoValidacao> Erros =>
            _erros.AsReadOnly();

        public HistoricoValidacao()
        {
            
        }


        public HistoricoValidacao(
            Guid? notaFiscalId,
            DateTime dataValidacao,
            bool valido,
            string xml)
        {
            if (string.IsNullOrWhiteSpace(xml))
                throw new ArgumentException(
                    "O XML deve ser informado.",
                    nameof(xml));

            Id = Guid.NewGuid();
            NotaFiscalId = notaFiscalId;
            DataValidacao = dataValidacao;
            Valido = valido;
            Xml = xml;
        }

        public void AdicionarErro(ErroHistoricoValidacao erro)
        {
            if (erro == null)
                throw new ArgumentNullException(nameof(erro));

            _erros.Add(erro);
        }
    }
}
