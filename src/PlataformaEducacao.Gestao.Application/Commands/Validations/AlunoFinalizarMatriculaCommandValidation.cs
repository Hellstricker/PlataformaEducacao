using FluentValidation;

namespace PlataformaEducacao.Gestao.Application.Commands.Validations
{
    public class AlunoFinalizarMatriculaCommandValidation : AbstractValidator<AlunoFinalizarMatriculaCommand>
    {
        public const string AlunoIdVazioMensagemErro = "O ID do aluno é obrigatório.";
        public const string CursoIdVazioMensagemErro = "O ID do curso é obrigatório.";        

        public AlunoFinalizarMatriculaCommandValidation()
        {
            RuleFor(c => c.AlunoId)
                .NotEmpty().WithMessage(AlunoIdVazioMensagemErro);
            RuleFor(c => c.CursoId)
                .NotEmpty().WithMessage(CursoIdVazioMensagemErro);            
        }
    }
}
