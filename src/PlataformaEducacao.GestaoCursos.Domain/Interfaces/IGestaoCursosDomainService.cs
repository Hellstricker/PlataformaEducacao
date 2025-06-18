namespace PlataformaEducacao.GestaoCursos.Domain.Interfaces
{
    public interface IGestaoCursosDomainService
    {
        Task CadastrarCurso(Curso curso);
        Task<bool> CadastrarAula(Guid cursoId, Aula aula);
    }
}
