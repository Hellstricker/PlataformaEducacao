using MediatR;
using PlataformaEducacao.Core.Data.EventSourcing;
using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.DomainEvents;
using PlataformaEducacao.Core.Messages.Notifications;

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
            await _eventSourcingRepository.SalvarEvento(evento);
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            await _mediator.Publish(notificacao);
        }

        public async Task EnviarCommand<T>(T command) where T : Command
        {
            await _mediator.Send(command);
        }

        public async Task PublicarDomainEvent<T>(T domainEvent) where T : DomainEvent
        {
            await _mediator.Publish(domainEvent);
        }
    }
}
