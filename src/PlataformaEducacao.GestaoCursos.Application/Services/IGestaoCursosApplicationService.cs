using PlataformaEducacao.GestaoCursos.Application.Dtos;

namespace PlataformaEducacao.GestaoCursos.Application.Services
{
    public interface IGestaoCursosApplicationService
    {
        Task CadastrarCurso(CadastrarCursoDto curso);
        Task<bool> CadastrarAula(Guid cursoId, CadastrarAulaDto aula);
        Task<CursoDto> ObterCursoPorId(Guid cursoId);
        Task<CursoDto> ObterCursoCompletoSemTrackingPorIdAsync(Guid cursoId);
        Task<IEnumerable<CursoDto>> ObterTodosCursos();
    }
}
