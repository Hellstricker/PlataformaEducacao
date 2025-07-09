using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.DomainEvents;
using PlataformaEducacao.Core.Messages.IntegrationEvents;

namespace PlataformaEducacao.Cadastros.Domain.Events
{
    public class AulaAdicionadaEvent : IntegrationEvent
    {
        public Guid CursoId { get; private set; }
        public string Titulo { get; private set; }
        public int Duracao { get; private set; }
        public string Conteudo { get; private set; }

        public AulaAdicionadaEvent(Guid cursoId, string titulo, int duracao, string conteudo)            
        {
            AggregateId = cursoId;
            CursoId = cursoId;
            Titulo = titulo;
            Duracao = duracao;
            Conteudo = conteudo;
        }
    }
}
