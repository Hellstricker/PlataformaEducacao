namespace PlataformaEducacao.Core.Messages.IntegrationEvents
{
    public class PagamentoRealizadoEvent : IntegrationEvent
    {
        public Guid MatriculaId { get; private set; }
        public Guid CursoId { get; private set; }
        public Guid AlunoId { get; private set; }
        public Guid PagamentoId { get; private set; }
        public Guid TransacaoId { get; private set; }
        public decimal Valor { get; private set; }

        public PagamentoRealizadoEvent(Guid matriculaId, Guid cursoId, Guid alunoId, Guid pagamentoId, Guid transacaoId, decimal valor)
        {
            AggregateId = pagamentoId;
            MatriculaId = MatriculaId;
            CursoId = cursoId;
            AlunoId = alunoId;
            PagamentoId = pagamentoId;
            TransacaoId = transacaoId;
            Valor = valor;
        }
    }
}
