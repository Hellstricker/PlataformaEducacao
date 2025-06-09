using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.Messages.DomainEvents;
using PlataformaEducacao.Core.Messages.Messages.Notifications;

namespace PlataformaEducacao.Core.Communications.Mediators
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        Task PublicarEventoDeDominio<T>(T notificacao) where T : DomainEvent;
    }
}
