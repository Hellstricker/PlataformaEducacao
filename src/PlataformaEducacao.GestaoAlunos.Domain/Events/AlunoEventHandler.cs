using MediatR;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Messages.Messages.IntegrationEvents;
using PlataformaEducacao.Core.Messages.Messages.Notifications;
using PlataformaEducacao.GestaoAlunos.Domain.Interfaces;

namespace PlataformaEducacao.GestaoAlunos.Domain.Events
{
    public class AlunoEventHandler :        
        INotificationHandler<PagamentoRealizadoEvent>,        
        INotificationHandler<ProgressoAtualizadoEvent>,
        INotificationHandler<CursoFinalizadoEvent>,
        INotificationHandler<AulaFinalizadaEvent>        
    {
        private readonly IMediatorHandler _mediator;
        private readonly IGestaoAlunosDomainService _gestaoAlunosDomainService;

        public AlunoEventHandler(IMediatorHandler mediator, IGestaoAlunosDomainService gestaoAlunosDomainService)
        {
            _mediator = mediator;
            _gestaoAlunosDomainService = gestaoAlunosDomainService;
        }

        public async Task Handle(PagamentoRealizadoEvent notification, CancellationToken cancellationToken)
        {
            await _gestaoAlunosDomainService.PagarMatricula(notification.AlunoId, notification.MatriculaId);
        }
        public async Task Handle(ProgressoAtualizadoEvent notification, CancellationToken cancellationToken)
        {
            await _gestaoAlunosDomainService.FinalizarCurso(notification.AlunoId, notification.CursoId);
        }

        public async Task Handle(CursoFinalizadoEvent notification, CancellationToken cancellationToken)
        {
            await _gestaoAlunosDomainService.GerarCertificado(notification.AlunoId, notification.CursoId);
        }

        public async Task Handle(AulaFinalizadaEvent notification, CancellationToken cancellationToken)
        {
            await _gestaoAlunosDomainService.AtualizarProgresso(notification.AlunoId, notification.CursoId, notification.AulaId, notification.TotalAulasCurso);
        }
    }
}
