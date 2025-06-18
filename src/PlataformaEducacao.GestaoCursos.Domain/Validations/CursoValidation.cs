using FluentValidation;

namespace PlataformaEducacao.GestaoCursos.Domain.Validations
{
    public class CursoValidation : AbstractValidator<Curso>
    {
        public CursoValidation()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("O nome do curso não pode ser vazio")
                .Length(2, 100).WithMessage("O nome do curso deve ter entre 2 e 100 caracteres");
            RuleFor(c => c.Valor)
                .GreaterThan(0).WithMessage("O valor do curso deve ser maior que zero");
            RuleFor(c => c.ConteudoProgramatico.Conteudo)
                .NotNull().WithMessage("O conteúdo não pode ser nulo");
            RuleFor(c => c.ConteudoProgramatico.Objetivo)
                .NotNull().WithMessage("O objetivo não pode ser nulo");
        }
    }
}
