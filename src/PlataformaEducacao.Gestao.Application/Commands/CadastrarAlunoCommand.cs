using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Gestao.Application.Commands.Validations;

namespace PlataformaEducacao.Gestao.Application.Commands
{
    public class CadastrarAlunoCommand:Command
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string ConfirmacaoSenha { get; private set; }

        public CadastrarAlunoCommand(string nome, string email, string senha, string confirmacaoSenha)
        {            
            Nome = nome;
            Email = email;
            Senha = senha;
            ConfirmacaoSenha = confirmacaoSenha;
        }

        public override bool EhValido()
        {
            ValidationResult = new CadastrarAlunoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
