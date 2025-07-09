using PlataformaEducacao.Core.Data;

namespace PlataformaEducacao.Gestao.Domain
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        void Adicionar(Aluno aluno);
        void Adicionar(Matricula matricula);
        void Atualizar(Matricula matricula);
        void Adicionar(Certificado certificado);
        Task<Matricula> ObterMatriculaParaPagamento(Guid alunoId, Guid cursoId);
        Task<Aluno> ObterPorIdAsync(Guid id);
    }
}
