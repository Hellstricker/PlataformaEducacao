using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.DomainObjects.Dtos;
using PlataformaEducacao.Core.Messages.IntegrationEvents;
using PlataformaEducacao.Core.Messages.Notifications;

namespace PlataformaEducacao.Pagamentos.Business
{
    public class PagamentoService : IPagamentoService
    {
        public static readonly string PagamentoRecusadoMessage = "Pagamento não realizado por recusa da operadora.";

        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IPagamentoCartaoCreditoFacade _pagamentoCartaoCreditoFacade;

        public PagamentoService(
            IPagamentoRepository pagamentoRepository,
            IMediatorHandler mediatorHandler,
            IPagamentoCartaoCreditoFacade pagamentoCartaoCreditoFacade)
        {
            _pagamentoRepository = pagamentoRepository;
            _mediatorHandler = mediatorHandler;
            _pagamentoCartaoCreditoFacade = pagamentoCartaoCreditoFacade;
        }
        public async Task<Transacao> RealizarPagamentoCurso(PagamentoMatriculaDto pagamentoMatricula)
        {
            var curso = new Curso
            {
                Id = pagamentoMatricula.CursoId,
                Valor = pagamentoMatricula.Valor
            };

            var pagamento = new Pagamento
            {
                AlunoId = pagamentoMatricula.AlunoId,
                CursoId = pagamentoMatricula.CursoId,
                Valor = pagamentoMatricula.Valor,
                DadosCartao = new DadosCartao
                {
                    NomeCartao = pagamentoMatricula.NomeCartao!,
                    NumeroCartao = pagamentoMatricula.NumeroCartao!,
                    MesAnoExpiracao = pagamentoMatricula.MesAnoExpiracao!,
                    Ccv = pagamentoMatricula.Ccv!
                }
            };

            var transacao = _pagamentoCartaoCreditoFacade.RealizarPagamento(curso, pagamento);

            if (transacao.StatusTransacao == StatusTransacao.Pago)
            {
                pagamento.AdicionarEvento(new PagamentoRealizadoEvent(pagamentoMatricula.AlunoId, pagamentoMatricula.CursoId, pagamento.Id, transacao.Id, pagamentoMatricula.Valor));
                pagamento.Status = transacao.StatusTransacao.ToString();
                _pagamentoRepository.Adicionar(pagamento);
                _pagamentoRepository.Adicionar(transacao);
                await _pagamentoRepository.UnitOfWork.CommitAsync();
                return transacao;
            }
            await _mediatorHandler.PublicarNotificacao(new DomainNotification("Pagamento", PagamentoRecusadoMessage));
            await _mediatorHandler.PublicarEvento(new PagamentoRecusadoEvent(pagamentoMatricula.AlunoId, pagamentoMatricula.CursoId, pagamento.Id, transacao.Id, pagamentoMatricula.Valor));
            return transacao;
        }
    }
}