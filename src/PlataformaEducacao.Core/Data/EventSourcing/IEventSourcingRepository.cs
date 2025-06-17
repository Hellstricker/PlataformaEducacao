using PlataformaEducacao.Core.Messages;

namespace PlataformaEducacao.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {        
        Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId);        
        Task SalvarEvento<TEvent>(TEvent storedEvent) where TEvent: Event;        
    }
}
