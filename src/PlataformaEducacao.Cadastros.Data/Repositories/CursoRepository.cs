using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Cadastros.Domain;
using PlataformaEducacao.Core.Data;

namespace PlataformaEducacao.Cadastros.Data.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly CursoContext _context;

        public CursoRepository(CursoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;


        public async Task<Curso> ObterPorIdAsync(Guid cursoId)
        {
            var curso = await _context.Cursos.FirstOrDefaultAsync(c => c.Id == cursoId);
            if (curso == null) return null;
            await _context.Entry(curso).Collection(c => c.Aulas).LoadAsync();
            return curso;
        }

        public void Adicionar(Curso curso)
        {
            _context.Cursos.Add(curso);
        }

        public void Adicionar(Aula aula)
        {
            _context.Aulas.Add(aula);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
