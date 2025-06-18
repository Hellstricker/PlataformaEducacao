using FluentValidation;

namespace PlataformaEducacao.GestaoAlunos.Domain.Validations
{
    public class AulaFinalizadaValidation : AbstractValidator<AulaFinalizada>
    {
        public AulaFinalizadaValidation()
        {
            RuleFor(a => a.AulaId)
                .NotEqual(Guid.Empty).WithMessage("O Id da aula precisa ser informado.");
            RuleFor(a => a.MatriculaId)
                .NotEqual(Guid.Empty).WithMessage("O Id da matrícula precisa ser informado.");            
        }
    }
}