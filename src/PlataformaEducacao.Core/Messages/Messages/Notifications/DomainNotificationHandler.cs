namespace PlataformaEducacao.Core.Messages.Messages.Notifications
{
    public class DomainNotificationHandler : IDomainNotificationHandler
    {
        private List<DomainNotification> _notifications;
        private Guid Id { get; set; } = Guid.NewGuid();

        public DomainNotificationHandler()
        {
            _notifications = new List<DomainNotification>();
        }

        public Task Handle(DomainNotification notification, CancellationToken cancellationToken)
        {
            _notifications.Add(notification);
            return Task.CompletedTask;
        }

        public virtual IReadOnlyCollection<DomainNotification> ObterNotificacoes()
        {
            return _notifications.AsReadOnly();
        }

        public virtual bool TemNotificacoes()
        {
            return ObterNotificacoes().Any();
        }

        public void Dispose()
        {
            _notifications.Clear();
        }
    }
}
