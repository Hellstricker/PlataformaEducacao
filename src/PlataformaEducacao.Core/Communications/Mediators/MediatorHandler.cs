using MediatR;
using PlataformaEducacao.Core.Data.EventSourcing;
using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.Messages.DomainEvents;
using PlataformaEducacao.Core.Messages.Messages.Notifications;

namespace PlataformaEducacao.Core.Communications.Mediators
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public MediatorHandler(IMediator mediator, IEventSourcingRepository eventSourcingRepository)
        {
            _mediator = mediator;
            _eventSourcingRepository = eventSourcingRepository;
        }
        
        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
            if (evento is DomainEvent domainEvent) return;
            await _eventSourcingRepository.SalvarEvento(evento);
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            await _mediator.Publish(notificacao);
        }
    }
}
