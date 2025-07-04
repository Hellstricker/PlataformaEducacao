using PlataformaEducacao.Core.Data;

namespace PlataformaEducacao.Gestao.Domain
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        void Adicionar(Aluno aluno);
        void Adicionar(Matricula matricula);
        void Atualizar(Matricula matricula);
        Task<Aluno> ObterPorIdAsync(Guid id);
    }
}
