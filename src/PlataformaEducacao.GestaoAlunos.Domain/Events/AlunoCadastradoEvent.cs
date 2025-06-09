using PlataformaEducacao.Core.Messages.Messages.DomainEvents;

namespace PlataformaEducacao.GestaoAlunos.Domain.Events
{
    public class AlunoCadastradoEvent : DomainEvent
    {
        public Guid AlunoId { get; private set; }
        public string Nome { get; private set; }
        public AlunoCadastradoEvent(Guid alunoId, string nome)
            :base(alunoId)
        {
            AlunoId = alunoId;
            Nome = nome;
        }
    }    
}
