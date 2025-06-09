using PlataformaEducacao.Core.Data;

namespace PlataformaEducacao.GestaoCursos.Domain.Interfaces
{
    public interface ICursoRepository : IRepository<Curso>
    {
        void Adicionar(Curso curso);
        void AdicionarAula(Aula aula);
        Task<Curso?> ObterCursoComAulasPorIdAsync(Guid cursoId);        
        Task<Curso?> ObterCursoPorIdAsync(Guid cursoId);
        Task<Curso?> ObterCursoCompletoSemTrackingPorIdAsync(Guid cursoId);
        Task<IEnumerable<Curso>> ObterTodosAsync();
    }
}
