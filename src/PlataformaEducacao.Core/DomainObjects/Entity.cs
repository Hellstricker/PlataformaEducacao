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
        protected Entity(Guid id)
        {
            Id = id;
            _notificacoes = new List<Event>();
        }


        public void AdicionarEvento(Event notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public void RemoverEvento(Event notificacao)
        {
            if (_notificacoes.Contains(notificacao))
            {
                _notificacoes.Remove(notificacao);
            }
        }

        public void LimparEventos()
        {
            _notificacoes.Clear();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var entity = (Entity)obj;
            return Id == entity.Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public static bool operator ==(Entity left, Entity right)
        {
            if (left is null && right is null) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }
        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public virtual bool EhValido()
        {
            throw new NotImplementedException("A classe derivada deve implementar o método EhValido para validar a entidade.");
        }
    }
}