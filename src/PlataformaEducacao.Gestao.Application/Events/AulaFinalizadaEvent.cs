using PlataformaEducacao.Core.Messages.IntegrationEvents;

namespace PlataformaEducacao.Gestao.Application.Events
{
    public class AulaFinalizadaEvent : IntegrationEvent
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public Guid AulaId { get; private set; }
        public int TotalAulasCurso { get; private set; }
        public decimal Progresso { get; private set; }
        public AulaFinalizadaEvent(Guid alunoId, Guid cursoId, Guid aulaId, int totalAulasCurso, decimal progresso)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            CursoId = cursoId;
            AulaId = aulaId;
            TotalAulasCurso = totalAulasCurso;
            Progresso = progresso;
        }
    }
}
