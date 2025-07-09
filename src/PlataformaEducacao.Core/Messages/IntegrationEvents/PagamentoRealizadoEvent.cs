namespace PlataformaEducacao.Core.Messages.IntegrationEvents
{
    public class PagamentoRealizadoEvent : IntegrationEvent
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }        
        public Guid PagamentoId { get; private set; }
        public Guid TransacaoId { get; private set; }
        public decimal Valor { get; private set; }

        public PagamentoRealizadoEvent(Guid alunoId, Guid cursoId, Guid pagamentoId, Guid transacaoId, decimal valor)
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
