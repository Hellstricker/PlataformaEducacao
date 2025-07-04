using FluentValidation;
using PlataformaEducacao.Gestao.Domain;

namespace PlataformaEducacao.Gestao.Application.Commands.Validations
{
    public class MatricularAlunoCommandValidation : AbstractValidator<MatricularAlunoCommand>
    {
        public readonly static string AlunoIdVazioMensagemErro = "O Id do aluno não pode ser vazio.";
        public readonly static string CursoIdVazioMensagemErro = "O Id do curso não pode ser vazio.";
        public readonly static string CursoNomeVazioMensagemErro = "O nome do curso não pode ser vazio.";
        public readonly static string CursoNomeTamanhoMensagemErro = $"O nome do curso deve ter entre {Matricula.MIN_NOMECURSO_CHAR} e {Matricula.MAX_NOMECURSO_CHAR} caracteres.";
        public readonly static string CursoValorInvalidoMensagemErro = "O valor do curso deve ser maior que zero.";        
        public readonly static string CursoTototalAulasInvalidoMensagemErro = "O total de aulas do curso deve ser maior que zero.";

        public MatricularAlunoCommandValidation()
        {
            RuleFor(m => m.AlunoId)
                .NotEqual(Guid.Empty)
                .WithMessage(AlunoIdVazioMensagemErro);
            RuleFor(m => m.CursoId)
                .NotEqual(Guid.Empty)
                .WithMessage(CursoIdVazioMensagemErro);
            RuleFor(m => m.CursoNome)
                .NotEmpty()
                .WithMessage(CursoNomeVazioMensagemErro)
                .Length(Matricula.MIN_NOMECURSO_CHAR, Matricula.MAX_NOMECURSO_CHAR)
                .WithMessage(CursoNomeTamanhoMensagemErro);
            RuleFor(m => m.CursoValor)
                .GreaterThan(0)
                .WithMessage(CursoValorInvalidoMensagemErro);
            RuleFor(m => m.CursoTotalAulas)
                .GreaterThan(0)
                .WithMessage(CursoTototalAulasInvalidoMensagemErro);
        }
    }
}
