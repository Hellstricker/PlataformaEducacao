using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Messages.Messages.Notifications;

namespace PlataformaEducacao.Core.DomainObjects
{
    public abstract class DomainService
    {
        protected readonly IMediatorHandler _mediator;

        protected DomainService(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        protected bool ValidarEntidade<T>(T entidade) where T : Entity
        {
            if (entidade is null)
            {
                _mediator.PublicarNotificacao(new DomainNotification($"{typeof(T).Name} inválido", $"{typeof(T).Name} não pode ser nulo(a)"));
                return false;
            }

            if (entidade.EhValido()) return true;

            foreach (var error in entidade.ValidationResult.Errors)
            {
                _mediator.PublicarNotificacao(new DomainNotification(error.PropertyName, error.ErrorMessage));
            }
            return false;
        }
    }
}
