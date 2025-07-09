using FluentValidation;

namespace PlataformaEducacao.Gestao.Domain.Validations
{
    public class AulaFinalizadaValidation : AbstractValidator<AulaFinalizada>
    {
        public static string AulaIdMensagem = "O Id da aula precisa ser informado.";        
        public static string CursoIdMensagem = "O Id do Curso precisa ser informado.";
        public AulaFinalizadaValidation()
        {
            RuleFor(a => a.MatriculaId)
                .NotEqual(Guid.Empty).WithMessage("O Id da matrícula precisa ser informado.");            
            RuleFor(a => a.AulaId)
                .NotEqual(Guid.Empty).WithMessage("O Id da aula precisa ser informado.");            
        }
    }
}
