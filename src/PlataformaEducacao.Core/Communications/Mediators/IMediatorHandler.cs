using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.Notifications;
using System.Threading.Tasks;

namespace PlataformaEducacao.Core.Communications.Mediators
{
    public interface IMediatorHandler
    {
        Task PublicarCommand<T>(T command) where T : Command;
        Task PublicarEvento<T>(T evento) where T : Event;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
    }
}
