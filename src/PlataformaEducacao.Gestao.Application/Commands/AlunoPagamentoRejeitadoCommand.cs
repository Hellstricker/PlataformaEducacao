using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Gestao.Application.Commands.Validations;

namespace PlataformaEducacao.Gestao.Application.Commands
{
    public class AlunoPagamentoRejeitadoCommand : Command
    {
        public AlunoPagamentoRejeitadoCommand(Guid alunoId, Guid cursoId)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
        }

        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new AlunoPagamentoRejeitadoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
