using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.GestaoAlunos.Domain
{
    public class AulaFinalizada : Entity
    {
        public Guid AulaId { get; private set; }
        public Guid MatriculaId { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public AulaFinalizada(Guid matriculaId, Guid aulaId)
        {
            AulaId = aulaId;
            MatriculaId = matriculaId;            
        }

        public Matricula Matricula { get; private set; }
    }
}