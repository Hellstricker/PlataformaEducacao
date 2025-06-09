using PlataformaEducacao.Core.Messages;

namespace PlataformaEducacao.Core.Messages.Messages.DomainEvents
{
    public abstract class DomainEvent : Event
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
