using PlataformaEducacao.Core.Data;

namespace PlataformaEducacao.GestaoAlunos.Domain.Interfaces
{
    public interface IAlunoRepository:IRepository<Aluno>
    {
        Task<IEnumerable<Aluno>> ObterAlunosCompletosSemTracking();
        Task<Aluno?> ObterAlunoParaMatriculaPorId(Guid alunoId);
        Task<Aluno?> ObterAlunoCompletoSemTrackingPorId(Guid alunoId);
        Task<Aluno?> ObterAlunoPorId(Guid alunoId);
        void Adicionar(Matricula matricula);
        void Adicionar(AulaFinalizada aulaFinalizada);
        void Adicionar(Aluno aluno);
        void Adicionar(Certificado certificado);
        Task<IEnumerable<Matricula>> ObterMatriculasPendentesPagamento(Guid alunoId);
        Task<Matricula?> ObterMatriculaPendentePagamento(Guid matriculaId);
        Task<Matricula> ObterMatriculaPorId(Guid matriculaId);
        void Atualizar(Matricula matricula);
               
    }
}
