using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Gestao.Application.Commands.Validations;

namespace PlataformaEducacao.Gestao.Application.Commands
{
    public class AlunoFinalizarAulaCommand : Command
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public Guid AulaId { get; private set; }

        public AlunoFinalizarAulaCommand(Guid alunoId, Guid cursoId, Guid aulaId)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            AulaId = aulaId;
        }

        public override bool EhValido()
        {
            ValidationResult = new AlunoFinalizarAulaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
