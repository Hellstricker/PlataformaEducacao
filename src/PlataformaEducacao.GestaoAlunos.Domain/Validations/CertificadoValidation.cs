using FluentValidation;

namespace PlataformaEducacao.GestaoAlunos.Domain.Validations
{
    public class CertificadoValidation : AbstractValidator<Certificado>
    {
        public CertificadoValidation()
        {
            RuleFor(c => c.NomeCurso)
                .NotEmpty().WithMessage("O nome do curso não pode ser vazio")
                .Length(2, 100).WithMessage("O nome do curso deve ter entre 2 e 100 caracteres");
            RuleFor(c => c.MatriculaId)
                .NotEqual(Guid.Empty).WithMessage("O Id da matrícula não pode ser vazio");
        }
    }
}
