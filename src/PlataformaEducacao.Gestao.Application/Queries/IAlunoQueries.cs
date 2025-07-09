using PlataformaEducacao.Gestao.Application.Queries.ViewModels;

namespace PlataformaEducacao.Gestao.Application.Queries
{
    public interface IAlunoQueries
    {
        Task<MatriculasAlunoViewModel> ObterMatriculasAluno(Guid AlunoId);
    }
}
