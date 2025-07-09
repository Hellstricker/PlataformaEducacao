using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Core.Data;
using PlataformaEducacao.Gestao.Domain;

namespace PlataformaEducacao.Gestao.Data.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly GestaoContext _context;
        
        public AlunoRepository(GestaoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
        }

        public void Adicionar(Matricula matricula)
        {
            _context.Matriculas.Add(matricula);
        }

        public void Atualizar(Matricula matricula)
        {
            _context.Matriculas.Update(matricula);
        }

        public void Adicionar(Certificado certificado)
        {
            _context.Certificados.Add(certificado);
        }

        public async Task<Aluno> ObterPorIdAsync(Guid alunoId)
        {
            var aluno = await _context.Alunos.FirstOrDefaultAsync(c => c.Id == alunoId);
            if (aluno == null) return null;
            await _context.Entry(aluno).Collection(a => a.Matriculas).LoadAsync();
            return aluno;
        }

        public async Task<Matricula> ObterMatriculaParaPagamento(Guid alunoId, Guid cursoId)
        {
            return await _context.Matriculas.AsNoTracking().FirstOrDefaultAsync(c => c.AlunoId == alunoId && c.Curso.CursoId == cursoId && c.Status == StatusMatriculaEnum.PENDENTE_PAGAMENTO);            
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        
    }
}
