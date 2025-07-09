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
        INotificationHandler<AulaFinalizadaEvent>,
        INotificationHandler<MatriculaFinalizadaEvent>
    {        
        private readonly IMediatorHandler _mediatorHandler;


        public AlunoEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;

        }

        public async Task Handle(PagamentoRealizadoEvent notification, CancellationToken cancellationToken)
        {
            var command = new AlunoPagarMatriculaCommand(notification.AlunoId, notification.CursoId);
            await _mediatorHandler.PublicarCommand(command);
        }

        public async Task Handle(PagamentoRecusadoEvent notification, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        public async Task Handle(AulaFinalizadaEvent notification, CancellationToken cancellationToken)
        {
            var command = new AlunoFinalizarMatriculaCommand(notification.AlunoId, notification.CursoId);
            await _mediatorHandler.PublicarCommand(command);
        }

        public async Task Handle(MatriculaFinalizadaEvent notification, CancellationToken cancellationToken)
        {
            var command = new AlunoGerarCertificadoCommand(notification.AlunoId, notification.CursoId);
            await _mediatorHandler.PublicarCommand(command);
        }
    }
}
