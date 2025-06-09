using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Core.Data;
using PlataformaEducacao.GestaoCursos.Domain;
using PlataformaEducacao.GestaoCursos.Domain.Interfaces;

namespace PlataformaEducacao.GestaoCursos.Data.Repositories
{
    public class CursoRepositiry : ICursoRepository
    {
        private readonly GestaoCursosContext _context;

        public CursoRepositiry(GestaoCursosContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public async Task<Curso?> ObterCursoPorIdAsync(Guid cursoId)
        {
            return await _context.Cursos                
                .FirstOrDefaultAsync(c => c.Id == cursoId);
        }

        public async Task<Curso?> ObterCursoComAulasPorIdAsync(Guid cursoId)
        {
            return await _context.Cursos
                .Include(c => c.Aulas)
                .FirstOrDefaultAsync(c => c.Id == cursoId);
        }

        public async Task<Curso?> ObterCursoCompletoSemTrackingPorIdAsync(Guid cursoId)
        {
            return await _context.Cursos
                .AsNoTracking()
                .Include(c => c.Aulas)                
                .FirstOrDefaultAsync(c => c.Id == cursoId);
        }

        public async Task<IEnumerable<Curso>> ObterTodosAsync()
        {
            return await _context.Cursos
                .AsNoTracking()
                .Include(c => c.Aulas)
                .ToListAsync();
        }

        public void Adicionar(Curso curso)
        {
            _context.Cursos.Add(curso);
        }


        public void AdicionarAula(Aula aula)
        {
            _context.Aulas.Add(aula);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
