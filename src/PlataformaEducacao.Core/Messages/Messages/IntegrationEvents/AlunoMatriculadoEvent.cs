namespace PlataformaEducacao.Core.Messages.Messages.IntegrationEvents
{
    public class AlunoMatriculadoEvent : IntegrationEvent
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public AlunoMatriculadoEvent(Guid alunoId, Guid cursoId)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            CursoId = cursoId;
        }
    }
}
