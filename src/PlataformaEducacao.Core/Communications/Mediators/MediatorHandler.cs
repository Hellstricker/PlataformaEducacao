using MediatR;
using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.Notifications;

namespace PlataformaEducacao.Core.Communications.Mediators
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;            
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);            
        }

        public async Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            await _mediator.Publish(notificacao);
        }

        public async Task PublicarCommand<T>(T command) where T : Command
        {
            await _mediator.Send(command);
        }
    }
}
