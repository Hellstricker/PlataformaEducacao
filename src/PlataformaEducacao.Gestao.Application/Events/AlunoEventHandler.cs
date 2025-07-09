using MediatR;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Messages.IntegrationEvents;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.Gestao.Application.Commands;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.Gestao.Application.Events
{
    public class AlunoEventHandler :
        INotificationHandler<PagamentoRealizadoEvent>,
        INotificationHandler<PagamentoRecusadoEvent>,
        INotificationHandler<AlunoAulaFinalizadaEvent>,
        INotificationHandler<AlunoMatriculaFinalizadaEvent>
    {        
        private readonly IMediatorHandler _mediatorHandler;


        public AlunoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;

        }

        public async Task Handle(PagamentoRealizadoEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public async Task Handle(PagamentoRecusadoEvent notification, CancellationToken cancellationToken)
        {
            var command = new AlunoPagamentoRejeitadoCommand(notification.AlunoId, notification.CursoId);
            await _mediatorHandler.EnviarCommand(command);
        }

        public async Task Handle(AlunoAulaFinalizadaEvent notification, CancellationToken cancellationToken)
        {
            var command = new AlunoFinalizarMatriculaCommand(notification.AlunoId, notification.CursoId);
            await _mediatorHandler.EnviarCommand(command);
        }

        public async Task Handle(AlunoMatriculaFinalizadaEvent notification, CancellationToken cancellationToken)
        {
            var command = new AlunoGerarCertificadoCommand(notification.AlunoId, notification.CursoId);
            await _mediatorHandler.EnviarCommand(command);
        }
    }
}
