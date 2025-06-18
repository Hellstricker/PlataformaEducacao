using FluentValidation;

namespace PlataformaEducacao.GestaoAlunos.Domain.Validations
{
    public class MatriculaValidation : AbstractValidator<Matricula>
    {
        public MatriculaValidation()
        {
            RuleFor(m => m.AlunoId)
                .NotEqual(Guid.Empty).WithMessage("O Id do aluno não pode ser vazio");
            RuleFor(m => m.CursoId)
                .NotEqual(Guid.Empty).WithMessage("O Id do curso não pode ser vazio");
            RuleFor(m => m.NomeCurso)
                .NotEmpty().WithMessage("O nome do curso não pode ser vazio")
                .Length(2, 100).WithMessage("O nome do curso deve ter entre 2 e 100 caracteres");
        }
    }
}