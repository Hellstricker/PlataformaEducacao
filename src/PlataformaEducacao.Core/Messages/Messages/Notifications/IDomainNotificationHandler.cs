using MediatR;

namespace PlataformaEducacao.Core.Messages.Messages.Notifications
{
    public interface IDomainNotificationHandler : INotificationHandler<DomainNotification>
    {
        IReadOnlyCollection<DomainNotification> ObterNotificacoes();
        bool TemNotificacoes();
        void Dispose();
    }
}
