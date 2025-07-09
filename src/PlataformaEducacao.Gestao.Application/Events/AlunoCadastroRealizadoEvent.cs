using PlataformaEducacao.Core.Messages;

namespace PlataformaEducacao.Gestao.Application.Events
{
    public class AlunoCadastroRealizadoEvent : Event
    {
        public Guid AlunoId { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public string ConfirmacaoSenha { get; private set; }    
        public AlunoCadastroRealizadoEvent(Guid alunoId, string nome, string email, string senha, string confirmacaoSenha)
        {
            AggregateId = alunoId;            
            AlunoId = alunoId;
            Nome = nome;
            Email = email;
            Senha = senha;
            ConfirmacaoSenha = confirmacaoSenha;
        }
    }
}
