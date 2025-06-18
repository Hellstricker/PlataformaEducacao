using FluentValidation;

namespace PlataformaEducacao.GestaoCursos.Domain.Validations
{
    public class AulaValidation : AbstractValidator<Aula>
    {
        public AulaValidation()
        {
            RuleFor(a => a.Titulo)
                .NotEmpty().WithMessage("O título da aula não pode ser vazio")
                .Length(2, 100).WithMessage("O título da aula deve ter entre 2 e 100 caracteres");
            RuleFor(a => a.Conteudo)
                .NotEmpty().WithMessage("O conteúdo da aula não pode ser vazio")
                .Length(10, 5000).WithMessage("O conteúdo da aula deve ter entre 10 e 5000 caracteres");
            RuleFor(a => a.TotalMinutos)
                .GreaterThan(0).WithMessage("O total de minutos da aula deve ser maior que zero");
            RuleFor(a => a.CursoId)
                .NotEqual(Guid.Empty).WithMessage("O curso precisa ser informado");
        }
    }
}
