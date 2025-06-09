using PlataformaEducacao.Core.Messages.Messages.DomainEvents;

namespace PlataformaEducacao.GestaoAlunos.Domain.Events
{
    public class CertificadoGeradoEvent: DomainEvent
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public Guid CertificadoId { get; private set; }
        public CertificadoGeradoEvent(Guid alunoId, Guid cursoId, Guid certificadoId)
            :base(alunoId)
        {            
            AlunoId = alunoId;
            CursoId = cursoId;
            CertificadoId = certificadoId;
        }
    }    
}
