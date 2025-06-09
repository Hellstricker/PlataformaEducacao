using PlataformaEducacao.Core.Messages.Messages.IntegrationEvents;

namespace PlataformaEducacao.GestaoAlunos.Domain.Interfaces
{
    public interface IGestaoAlunosDomainService
    {
        Task MatricularAluno(Guid alunoid, Guid cursoId, string nomeCurso);
        Task PagarMatricula(Guid alunoId, Guid matriculaId);
        Task FinalizarAula(Guid alunoId, Guid cursoId, Guid aulaId, int totalAulasCurso);
        Task AtualizarProgresso(Guid alunoId, Guid cursoId, Guid aulaId, int totalAulas);
        Task FinalizarCurso(Guid alunoId, Guid matriculaId);
        Task GerarCertificado(Guid alunoId, Guid cursoId);
        Task CadastrarAluno(Guid Id, string nome);        
    }
}
