namespace PlataformaEducacao.Core.Messages.IntegrationEvents
{
    public class PagamentoRecusadoEvent : IntegrationEvent
    {
        public const string PagamentoRecusadoMessage = "Pagamento recusado pela operadora.";

        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }        
        public Guid PagamentoId { get; private set; }
        public Guid TransacaoId { get; private set; }
        public decimal Valor { get; private set; }

        public PagamentoRecusadoEvent(Guid alunoId, Guid cursoId, Guid pagamentoId, Guid transacaoId, decimal valor)
        {
            AggregateId = pagamentoId;
            AlunoId = alunoId;
            CursoId = cursoId;            
            PagamentoId = pagamentoId;
            TransacaoId = transacaoId;
            Valor = valor;
        }
    }
}
