using PlataformaEducacao.Core.Messages;

namespace PlataformaEducacao.Gestao.Application.Events
{
    public class AlunoMatriculaRealizadaEvent : Event
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public string? CursoNome { get; private set; }
        public decimal CursoValor { get; private set; }
        public int CursoTotalAulas { get; private set; }

        public AlunoMatriculaRealizadaEvent(Guid alunoId, Guid cursoId, string? cursoNome, decimal cursoValor, int cursoTotalAulas)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            CursoId = cursoId;
            CursoNome = cursoNome;
            CursoValor = cursoValor;
            CursoTotalAulas = cursoTotalAulas;
        }
    }
}
