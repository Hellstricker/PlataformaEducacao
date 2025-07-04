using PlataformaEducacao.Core.Data;

namespace PlataformaEducacao.Cadastros.Domain
{
    public interface ICursoRepository : IRepository<Curso>
    {
        void Adicionar(Curso curso);
        void Adicionar(Aula aula);
        Task<Curso> ObterPorIdAsync(Guid ic);
    }
}
