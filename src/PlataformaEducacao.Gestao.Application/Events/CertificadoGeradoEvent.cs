using PlataformaEducacao.Core.Messages;

namespace PlataformaEducacao.Gestao.Application.Events
{
    public class CertificadoGeradoEvent : Event
    {
        public Guid AlunoId { get; set; }
        public Guid NumeroCertificado { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string NomeCurso { get; private set; }        

        public CertificadoGeradoEvent(Guid alunoId, Guid numeroCertificado, string nomeCurso, DateTime dataCadastro)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            NumeroCertificado = numeroCertificado;
            DataCadastro = dataCadastro;
            NomeCurso = nomeCurso;
        }
    }
}
