using FluentValidation;

namespace PlataformaEducacao.Gestao.Application.Commands.Validations
{
    public class AlunoFinalizarAulaCommandValidation : AbstractValidator<AlunoFinalizarAulaCommand>
    {
        public const string AlunoIdVazioMensagemErro = "O ID do aluno é obrigatório.";
        public const string CursoIdVazioMensagemErro = "O ID do curso é obrigatório.";
        public const string AulaIdVazioMensagemErro = "O ID da aula é obrigatório.";

        public AlunoFinalizarAulaCommandValidation()
        {
            RuleFor(c => c.AlunoId)
                .NotEmpty().WithMessage(AlunoIdVazioMensagemErro);
            RuleFor(c => c.CursoId)
                .NotEmpty().WithMessage(CursoIdVazioMensagemErro);
            RuleFor(c => c.AulaId)
                .NotEmpty().WithMessage(AulaIdVazioMensagemErro);
        }
    }
}
