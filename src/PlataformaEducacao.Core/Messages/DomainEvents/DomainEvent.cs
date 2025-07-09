using MediatR;

namespace PlataformaEducacao.Core.Messages.DomainEvents
{
    public abstract class DomainEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
            Timestamp = DateTime.Now;
        }
    }
}
