using MediatR;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Messages.IntegrationEvents;
using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.Gestao.Domain;
using PlataformaEducacao.Gestao.Application.Events;

namespace PlataformaEducacao.Gestao.Application.Commands
{
    public class AlunoCommandHandler : 
        IRequestHandler<CadastrarAlunoCommand, bool>,
        IRequestHandler<MatricularAlunoCommand, bool>,
        IRequestHandler<AlunoPagarMatriculaCommand, bool>,
        IRequestHandler<AlunoFinalizarAulaCommand, bool>,
        IRequestHandler<AlunoFinalizarMatriculaCommand, bool>,
        IRequestHandler<AlunoGerarCertificadoCommand, bool>,
        IRequestHandler<AlunoPagamentoRejeitadoCommand, bool>
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
            aluno.AdicionarEvento(new AlunoCadastroRealizadoEvent(aluno.Id, aluno.Nome, aluno.Email, message.Senha, message.ConfirmacaoSenha));
            return await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(MatricularAlunoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;
            var matricula = new Matricula(message.AlunoId, message.CursoId, message.CursoNome, message.CursoValor, message.CursoTotalAulas);
            var aluno = await _alunoRepository.ObterPorIdAsync(message.AlunoId);

            if (!await ValidarSeAlunoExiste(aluno)) return false;

            if (!await ValidarSeAlunoNaoEstaMatriculado(aluno, message.CursoId)) return false;
            
            aluno.Matricular(matricula);
            aluno.AdicionarEvento(new AlunoMatriculaRealizadaEvent(message.AlunoId, message.CursoId, message.CursoNome, message.CursoValor, message.CursoTotalAulas));

            _alunoRepository.Adicionar(matricula);
            return await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(AlunoPagarMatriculaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;
            var aluno = await _alunoRepository.ObterPorIdAsync(message.AlunoId);

            if (!await ValidarSeAlunoExiste(aluno)) return false;

            if (!await ValidarSeAlunoEstaMatriculado(aluno, message.CursoId)) return false;

            var matricula = aluno.ObterMatricula(message.CursoId);

            if(!await ValidarSeMatriculaPodeSerPaga(matricula)) return false;                

            aluno.PagarMatricula(message.CursoId);
            aluno.AdicionarEvento(new AlunoPagouMatriculaEvent(matricula.AlunoId, message.CursoId,matricula.Curso.CursoValor, message.NomeCartao, message.NumeroCartao, message.MesAnoExpiracao, message.Ccv));
            _alunoRepository.Atualizar(matricula);
            return await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(AlunoPagamentoRejeitadoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;
            var aluno = await _alunoRepository.ObterPorIdAsync(message.AlunoId);

            if (!await ValidarSeAlunoExiste(aluno)) return false;

            if (!await ValidarSeAlunoEstaMatriculado(aluno, message.CursoId)) return false;

            var matricula = aluno.ObterMatricula(message.CursoId);

            if (!await ValidarSeMatriculaTerPagamentoCancelado(matricula)) return false;

            aluno.CancelarPagamentoMatricula(message.CursoId);
            aluno.AdicionarEvento(new AlunoPagamentoCanceladoEvent(matricula.AlunoId, message.CursoId, matricula.Curso.CursoValor));
            _alunoRepository.Atualizar(matricula);
            return await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(AlunoFinalizarAulaCommand message, CancellationToken cancellationToken)
        {
            if(!ValidarComando(message))return false;
            var aluno = await _alunoRepository.ObterPorIdAsync(message.AlunoId);

            if (!await ValidarSeAlunoExiste(aluno)) return false;

            if (!await ValidarSeAlunoEstaMatriculado(aluno, message.CursoId)) return false;

            var matricula = aluno.ObterMatricula(message.CursoId);

            if (!await ValidarSeMatriculaPodeFinalizarAula(matricula)) return false;

            aluno.FinalizarAula(message.CursoId, message.AulaId);            
            matricula.AdicionarEvento(new AlunoAulaFinalizadaEvent(aluno.Id, message.CursoId, message.AulaId, matricula.Curso.CursoTotalAulas, matricula.Curso.HistoricoAprendizado!.Progresso!.Value));
            _alunoRepository.Atualizar(matricula);            
            return await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(AlunoFinalizarMatriculaCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;
            var aluno = await _alunoRepository.ObterPorIdAsync(message.AlunoId);

            if (!await ValidarSeAlunoExiste(aluno)) return false;

            if (!await ValidarSeAlunoEstaMatriculado(aluno, message.CursoId)) return false;

            var matricula = aluno.ObterMatriculaParaFinalizar(message.CursoId);
            
            if(matricula == null) return false;

            aluno.FinalizarMatricula(message.CursoId);
            matricula.AdicionarEvento(new AlunoMatriculaFinalizadaEvent(aluno.Id, message.CursoId));
            _alunoRepository.Atualizar(matricula);
            return await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(AlunoGerarCertificadoCommand message, CancellationToken cancellationToken)
        {
            if (!ValidarComando(message)) return false;
            var aluno = await _alunoRepository.ObterPorIdAsync(message.AlunoId);

            if (!await ValidarSeAlunoExiste(aluno)) return false;

            if (!await ValidarSeAlunoEstaMatriculado(aluno, message.CursoId)) return false;

            var matricula = aluno.ObterMatriculaCursoFinalizado(message.CursoId);

            if (matricula == null) return false;

            aluno.GerarCertificado(message.CursoId);
            var certificado = aluno.Certificados.First(c => c.NomeCurso == matricula.Curso.CursoNome);
            certificado.AdicionarEvento(new AlunoCertificadoGeradoEvent(certificado.AlunoId, certificado.NumeroCertificado, certificado.NomeCurso, certificado.DataCadastro ));
            _alunoRepository.Adicionar(certificado);
            return await _alunoRepository.UnitOfWork.CommitAsync();
        }



        private async Task<bool> ValidarSeMatriculaPodeFinalizarAula(Matricula matricula)
        {
            return await Validar(matricula.PodeFinalizarAula(), Matricula.MatriculaNaoEstaEmAndamento);
        }
        private async Task<bool> ValidarSeMatriculaPodeSerPaga(Matricula matricula)
        {
            return await Validar(matricula.PodeSerPaga(), Matricula.MatriculaNaoEstaPendente);
        }
        private async Task<bool> ValidarSeMatriculaTerPagamentoCancelado(Matricula matricula)
        {
            return await Validar(matricula.EstaPaga(), Matricula.MatriculaNaoEstaPaga);
        }
        private async Task<bool> ValidarSeAlunoEstaMatriculado(Aluno aluno, Guid cursoId)
        {
            return await Validar(aluno.EstaMatriculado(cursoId), Aluno.AlunoNaoEstaMatriculado);            
        }
        private async Task<bool> ValidarSeAlunoNaoEstaMatriculado(Aluno aluno, Guid cursoId)
        {
            return await Validar(!aluno.EstaMatriculado(cursoId), Aluno.AlunoJaMatriculado);            
        }
        private async Task<bool> Validar(bool condicao, string mensagemSeFalso)
        {
            if (!condicao)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(this.GetType().Name, mensagemSeFalso));
                return false;
            }
            return true;
        }
        private async Task<bool> ValidarSeAlunoExiste(Aluno aluno)
        {
            if (aluno == null)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(this.GetType().Name, AlunoNaoEncontrado));
                return false;
            }
            return true;
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
