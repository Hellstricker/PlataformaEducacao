using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace PlataformaEducacao.Gestao.Domain.Validations
{
    public class AlunoValidation : AbstractValidator<Aluno>
    {

        public static string NomeVazioMensagemErro = "O nome do aluno é obrigatório.";
        public static string NomeTamanhoMensagemErro = $"O nome do aluno deve ter entre {Aluno.NOME_MIN_CARACTERES} e {Aluno.NOME_MAX_CARACTERES} caracteres.";
        public static string EmailVazioMensagemErro = "O email do aluno é obrigatório.";
        public static string EmailFormatoMensagemErro = "O email do aluno deve ser um endereço de email válido.";
        public AlunoValidation()
        {
            RuleFor(a => a.Nome)
                .NotEmpty().WithMessage(NomeVazioMensagemErro)
                .Length(Aluno.NOME_MIN_CARACTERES, Aluno.NOME_MAX_CARACTERES).WithMessage(NomeTamanhoMensagemErro);
            RuleFor(a => a.Email)
                .NotEmpty().WithMessage(EmailVazioMensagemErro)
                .EmailAddress().WithMessage(EmailFormatoMensagemErro);
        }
    }
}
