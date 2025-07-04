using FluentValidation.Results;
using PlataformaEducacao.Core.Messages;


namespace PlataformaEducacao.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }
        private List<Event> _notificacoes;
        public IReadOnlyCollection<Event> Notificacoes => _notificacoes.AsReadOnly();
        public ValidationResult ValidationResult { get; set; }
        protected Entity()
        {
            Id = Guid.NewGuid();
            _notificacoes = new List<Event>();
        }

        public void AdicionarEvento(Event notificacao)
        {
            _notificacoes ??= new List<Event>();
            _notificacoes.Add(notificacao);
        }

        public void LimparEventos()
        {
            _notificacoes?.Clear();
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException("A classe derivada deve implementar o método EhValido para validar a entidade.");
        }
    }
}
