using MediatR;

namespace PlataformaEducacao.Core.Messages.Messages.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public Guid Id { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }

        public DomainNotification(string key, string value, int version = 1)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Key = key;
            Value = value;
            Version = version;
        }
    }
}
