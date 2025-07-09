using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Gestao.Application.Commands.Validations;

namespace PlataformaEducacao.Gestao.Application.Commands
{
    public class AlunoGerarCertificadoCommand : Command
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }

        public AlunoGerarCertificadoCommand(Guid alunoId, Guid cursoId)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
        }

        public override bool EhValido()
        {
            ValidationResult = new AlunoGerarCertificadoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
