using PlataformaEducacao.Core.Messages;

namespace PlataformaEducacao.Gestao.Application.Events
{
    public class AlunoPagamentoCanceladoEvent : Event
    {        

        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public decimal CursoValor { get; private set; }

        public AlunoPagamentoCanceladoEvent(Guid alunoId, Guid cursoId, decimal cursoValor)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            CursoId = cursoId;
            CursoValor = cursoValor;
        }
    }
}
