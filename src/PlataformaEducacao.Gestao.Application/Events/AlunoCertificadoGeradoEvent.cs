using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.DomainEvents;
using PlataformaEducacao.Core.Messages.IntegrationEvents;

namespace PlataformaEducacao.Gestao.Application.Events
{
    public class AlunoCertificadoGeradoEvent : Event
    {
        public Guid AlunoId { get; set; }
        public Guid NumeroCertificado { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string NomeCurso { get; private set; }        

        public AlunoCertificadoGeradoEvent(Guid alunoId, Guid numeroCertificado, string nomeCurso, DateTime dataCadastro)            
        {            
            AggregateId = alunoId;
            AlunoId = alunoId;
            NumeroCertificado = numeroCertificado;
            DataCadastro = dataCadastro;
            NomeCurso = nomeCurso;
        }
    }
}
