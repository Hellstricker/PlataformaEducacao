using PlataformaEducacao.Core.Messages;

namespace PlataformaEducacao.Gestao.Application.Events
{
    public class MatriculaFinalizadaEvent : Event
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }

        public MatriculaFinalizadaEvent(Guid alunoId, Guid cursoId)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            CursoId = cursoId;
        }
    }
}
