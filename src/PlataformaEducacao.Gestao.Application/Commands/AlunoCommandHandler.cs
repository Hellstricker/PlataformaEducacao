using MediatR;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Messages.IntegrationEvents;
using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.Gestao.Domain;

namespace PlataformaEducacao.Gestao.Application.Commands
{
    public class AlunoCommandHandler : 
        IRequestHandler<CadastrarAlunoCommand, bool>,
        IRequestHandler<MatricularAlunoCommand, bool>,
        IRequestHandler<AlunoPagarMatriculaCommand, bool>
    {
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMediatorHandler _mediatorHandler;
        public const string AlunoNaoEncontrado = "Aluno não encontrado.";
        public AlunoCommandHandler(IAlunoRepository alunoRepository, IMediatorHandler mediatorHandler)
        {
            _alunoRepository = alunoRepository;
            _mediatorHandler = mediatorHandler;
        }
        public async Task<bool> Handle(CadastrarAlunoCommand message, CancellationToken cancellationToken)
        {            
            if (!ValidarComando(message)) return false;
            var aluno = new Aluno(message.Nome, message.Email);
            _alunoRepository.Adicionar(aluno);
            aluno.AdicionarEvento(new AlunoCadastradoEvent(aluno.Id, aluno.Nome, aluno.Email, message.Senha, message.ConfirmacaoSenha));            
            return await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(MatricularAlunoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;
            var matricula = new Matricula(message.AlunoId, message.CursoId, message.CursoNome, message.CursoValor, message.CursoTotalAulas);
            var aluno = await _alunoRepository.ObterPorIdAsync(message.AlunoId);
            
            if (aluno == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, AlunoNaoEncontrado));
                return false;
            }
            
            if (aluno.EstaMatriculado(message.CursoId))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, Aluno.AlunoJaMatriculado));
                return false;
            }
            
            aluno.Matricular(matricula);

            _alunoRepository.Adicionar(matricula);
            return await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(AlunoPagarMatriculaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;
            var aluno = await _alunoRepository.ObterPorIdAsync(message.AlunoId);
            
            if (aluno == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, AlunoNaoEncontrado));
                return false;
            }
            
            if (!aluno.EstaMatriculado(message.CursoId))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(this.GetType().ToString(), Aluno.AlunoNaoEstaMatriculado));
                return false;
            }

            var matricula = aluno.ObterMatricula(message.CursoId);

            if (!matricula.PodeSerPaga())
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(this.GetType().ToString(), Matricula.MatriculaNaoEstaPendente));
                return false;
            }

            aluno.PagarMatricula(message.CursoId);
            matricula = aluno.ObterMatricula(message.CursoId);
            _alunoRepository.Atualizar(matricula);
            return await _alunoRepository.UnitOfWork.CommitAsync();
        }

        private bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediatorHandler.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }
            return false;
        }

        
    }    
}
