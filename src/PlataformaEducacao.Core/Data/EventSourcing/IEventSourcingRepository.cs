using PlataformaEducacao.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlataformaEducacao.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId);
    }
}
