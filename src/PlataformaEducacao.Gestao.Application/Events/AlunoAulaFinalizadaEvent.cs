using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.DomainEvents;
using PlataformaEducacao.Core.Messages.IntegrationEvents;

namespace PlataformaEducacao.Gestao.Application.Events
{
    public class AlunoAulaFinalizadaEvent : Event
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public Guid AulaId { get; private set; }
        public int TotalAulasCurso { get; private set; }
        public decimal Progresso { get; private set; }
        public AlunoAulaFinalizadaEvent(Guid alunoId, Guid cursoId, Guid aulaId, int totalAulasCurso, decimal progresso)            
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
