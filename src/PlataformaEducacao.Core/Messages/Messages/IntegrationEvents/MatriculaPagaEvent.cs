namespace PlataformaEducacao.Core.Messages.Messages.IntegrationEvents
{
    public class MatriculaPagaEvent:IntegrationEvent
    {
        public Guid AlunoId { get; private set; }
        public Guid MatriculaId { get; private set; }
        public MatriculaPagaEvent(Guid alunoId, Guid matriculaId)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            MatriculaId = matriculaId;
        }
    }    
}
