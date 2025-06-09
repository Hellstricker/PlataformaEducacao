using PlataformaEducacao.Core.Messages.Messages.DomainEvents;

namespace PlataformaEducacao.GestaoAlunos.Domain.Events
{
    public class CursoFinalizadoEvent : DomainEvent
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public CursoFinalizadoEvent(Guid alunoId, Guid cursoId)
            : base(alunoId)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            CursoId = cursoId;
        }
    }
}
