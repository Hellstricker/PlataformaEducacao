using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Gestao.Application.Commands.Validations;

namespace PlataformaEducacao.Gestao.Application.Commands
{
    public class AlunoPagarMatriculaCommand:Command
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public AlunoPagarMatriculaCommand(Guid alunoId, Guid cursoId)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
        }
        public override bool EhValido()
        {
            ValidationResult = new AlunoPagarMatriculaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
