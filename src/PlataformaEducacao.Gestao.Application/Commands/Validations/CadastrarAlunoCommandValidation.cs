using FluentValidation;
using PlataformaEducacao.Gestao.Domain;

namespace PlataformaEducacao.Gestao.Application.Commands.Validations
{
    public class CadastrarAlunoCommandValidation: AbstractValidator<CadastrarAlunoCommand>
    {
        public readonly static string NomeVazioMensagemErro = "O nome do aluno é obrigatório.";
        public readonly static string NomeTamanhoMensagemErro = $"O nome do aluno deve ter entre {Aluno.NOME_MIN_CARACTERES} e {Aluno.NOME_MAX_CARACTERES} caracteres.";
        public readonly static string EmailVazioMensagemErro = "O email do aluno é obrigatório.";
        public readonly static string EmailFormatoMensagemErro = "O email do aluno deve ser um endereço de email válido.";
        
        public CadastrarAlunoCommandValidation()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage(NomeVazioMensagemErro)
                .Length(2, 100).WithMessage(NomeTamanhoMensagemErro);
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage(EmailVazioMensagemErro)
                .EmailAddress().WithMessage(EmailFormatoMensagemErro);
        }
    }
}
