using PlataformaEducacao.GestaoAlunos.Application.Dtos;
using PlataformaEducacao.GestaoAlunos.Domain.Interfaces;

namespace PlataformaEducacao.GestaoAlunos.Application.Services
{
    public interface IGestaoAlunosApplicationService
    {
        Task MatricularAluno(Guid alunoId, MatricularAlunoDto matriculaDto);
        Task<MatriculaParaPagamentoDto> ObterMatriculaParaPagamentoPorId(Guid matriculaId);
        Task<IEnumerable<MatriculaParaPagamentoDto>> ObterMatriculasPendentesPagamento(Guid alunoId);
        Task FinalizarAula(Guid alunoId, Guid cursoId, Guid aulaId, int totalAulasCurso);
        Task CadastrarAluno(Guid id, string nome);
        Task<AlunoCompletoDto> ObterAlunoPorId(Guid alunoId);
        Task<IEnumerable<AlunoCompletoDto>> ObterAlunosCompletosSemTracking();
    }
}
