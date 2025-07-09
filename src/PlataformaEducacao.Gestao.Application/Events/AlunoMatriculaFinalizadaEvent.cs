using PlataformaEducacao.Core.Messages;

namespace PlataformaEducacao.Gestao.Application.Events
{
    public class AlunoMatriculaFinalizadaEvent : Event
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }

        public AlunoMatriculaFinalizadaEvent(Guid alunoId, Guid cursoId)            
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            CursoId = cursoId;
        }
    }
}
