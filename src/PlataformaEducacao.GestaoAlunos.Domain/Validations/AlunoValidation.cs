using FluentValidation;

namespace PlataformaEducacao.GestaoAlunos.Domain.Validations
{
    public class AlunoValidation : AbstractValidator<Aluno>
    {
        public AlunoValidation()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage("O nome do(a) aluno(a) não pode ser vazio.")
                .Length(2, 100).WithMessage("O nome do aluno deve ter entre 2 e 100 caracteres.");
            RuleFor(a => a.Id)
                .NotEqual(Guid.Empty).WithMessage("O Id do(a) aluno(a) não pode ser vazio.");
        }
    }
}
