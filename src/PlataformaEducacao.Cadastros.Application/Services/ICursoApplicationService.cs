using PlataformaEducacao.Cadastros.Application.ViewModels;

namespace PlataformaEducacao.Cadastros.Application.Services
{
    public interface ICursoApplicationService
    {
        Task<bool> AdicionarAula(AulaViewModel aulaViewModel);
        Task<bool> AdicionarCurso(CursoViewModel cursoViewModel);
    }
}