using EventStore.ClientAPI;
using Newtonsoft.Json;
using PlataformaEducacao.Core.Data.EventSourcing;
using PlataformaEducacao.Core.Messages;
using System.Text;


namespace EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;        

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        

        }

        public async Task<IEnumerable<StoredEvent>> ObterEventos(Guid aggregateId)
        {
            var eventos = await _eventStoreService.GetConnection().ReadStreamEventsForwardAsync(
                aggregateId.ToString(),
                StreamPosition.Start,
                500,
                false);


            var listaEventos = new List<StoredEvent>();
            foreach (var evento in eventos.Events)
            {
                var dados = Encoding.UTF8.GetString(evento.Event.Data);
                var tipo = evento.Event.EventType;
                var jsonData = JsonConvert.DeserializeObject<BaseEvent>(dados);
                var dataOcorrencia = jsonData.Timestamp;
                listaEventos.Add(new StoredEvent(evento.Event.EventId, tipo, dataOcorrencia, dados));
            }

            return listaEventos;
        }

        public async Task SalvarEvento<TEvent>(TEvent evento) where TEvent : Event
        {
            await _eventStoreService.GetConnection().AppendToStreamAsync(
                evento.AggregateId.ToString(),
                ExpectedVersion.Any,
                FormatarEvento(evento));
        }

        private static IEnumerable<EventData> FormatarEvento<TEvent>(TEvent storedEvent) where TEvent : Event
        {            
            yield return new EventData(
                Guid.NewGuid(),
                storedEvent.GetType().Name,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(storedEvent)),
                null);

        }
    }
}
