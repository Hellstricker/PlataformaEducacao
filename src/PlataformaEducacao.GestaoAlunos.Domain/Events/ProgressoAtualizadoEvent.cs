using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.Messages.DomainEvents;

namespace PlataformaEducacao.GestaoAlunos.Domain.Events
{
    public class ProgressoAtualizadoEvent : DomainEvent
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }

        public ProgressoAtualizadoEvent(Guid alunoId, Guid cursoId) 
            : base(alunoId)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
        }
    }
}
