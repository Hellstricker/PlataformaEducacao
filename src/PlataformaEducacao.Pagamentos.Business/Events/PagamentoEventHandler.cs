using MediatR;
using PlataformaEducacao.Core.DomainObjects.Dtos;
using PlataformaEducacao.Core.Messages.IntegrationEvents;

namespace PlataformaEducacao.Pagamentos.Business.Events
{
    public class PagamentoEventHandler : INotificationHandler<AlunoPagouMatriculaEvent>
    {
        private readonly IPagamentoService _pagamentoService;

        public PagamentoEventHandler(IPagamentoService pagamentoService)
        {
            _pagamentoService = pagamentoService;
        }

        public async Task Handle(AlunoPagouMatriculaEvent notification, CancellationToken cancellationToken)
        {
            await _pagamentoService.RealizarPagamentoCurso(new PagamentoMatriculaDto(notification.AlunoId, notification.CursoId, notification.Valor, notification.NomeCartao, notification.NumeroCartao, notification.MesAnoExpiracao, notification.Ccv));
        }
    }
}
