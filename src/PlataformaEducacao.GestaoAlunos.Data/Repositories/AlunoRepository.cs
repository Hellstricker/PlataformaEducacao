using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Core.Data;
using PlataformaEducacao.GestaoAlunos.Domain;
using PlataformaEducacao.GestaoAlunos.Domain.Interfaces;

namespace PlataformaEducacao.GestaoAlunos.Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly GestaoAlunosContext _context;

        public AlunoRepository(GestaoAlunosContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public async Task<Aluno?> ObterAlunoParaMatriculaPorId(Guid alunoId)
        {
            return await _context.Alunos                
                .Include(a => a.Matriculas)
                .FirstOrDefaultAsync(a => a.Id == alunoId);
        }

        public async Task<Aluno?> ObterAlunoPorId(Guid alunoId)
        {
            return await _context.Alunos
                .Include(a => a.Matriculas)
                .ThenInclude(m => m.AulasFinalizadas)
                .FirstOrDefaultAsync(a => a.Id == alunoId);
        }

        public async Task<Aluno?> ObterAlunoCompletoSemTrackingPorId(Guid alunoId)
        {
            return await _context.Alunos
                .AsNoTracking()
                .Include(a => a.Matriculas)
                .ThenInclude(m => m.AulasFinalizadas)
                .Include(a => a.Matriculas)
                .ThenInclude(m => m.Certificado)
                .FirstOrDefaultAsync(a => a.Id == alunoId);
        }
        public async Task<IEnumerable<Aluno>> ObterAlunosCompletosSemTracking()
        {
            return await _context.Alunos
                .AsNoTracking()
                .Include(a => a.Matriculas)
                .ThenInclude(m => m.AulasFinalizadas)
                .Include(a => a.Matriculas)
                .ThenInclude(m => m.Certificado)
                .ToListAsync();
        }


        public async Task<IEnumerable<Matricula>> ObterMatriculasPendentesPagamento(Guid alunoId)
        {
            return await _context.Matriculas
                .AsNoTracking()
                .Where(m => m.AlunoId == alunoId && m.StatusMatricula == StatusMatricula.PENDENTE_PAGAMENTO)
                .ToListAsync();
        }

        public async Task<Matricula> ObterMatriculaPorId(Guid matriculaId)
        {
            return await _context.Matriculas.FirstAsync(m => m.Id == matriculaId);
        }

        public async Task<Matricula?> ObterMatriculaPendentePagamento(Guid matriculaId)
        {
            return await _context.Matriculas
                .AsNoTracking()
                .Where(m => m.Id == matriculaId && m.StatusMatricula == StatusMatricula.PENDENTE_PAGAMENTO)
                .FirstOrDefaultAsync();
        }

        public void Adicionar(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
        }

        public void Atualizar(Matricula matricula)
        {
            _context.Matriculas.Update(matricula);
        }

        public void Adicionar(AulaFinalizada aulaFinalizada)
        {
            _context.AulasFinalizadas.Add(aulaFinalizada);
        }

        public void Adicionar(Certificado certificado)
        {
            _context.Certificados.Add(certificado);
        }

        public void Adicionar(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
        }

        public void Dispose()
        {
            _context.Dispose();
        }        
    }
}
