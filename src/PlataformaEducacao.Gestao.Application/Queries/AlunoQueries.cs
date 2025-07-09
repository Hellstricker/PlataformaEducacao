using PlataformaEducacao.Gestao.Application.Queries.ViewModels;
using PlataformaEducacao.Gestao.Domain;

namespace PlataformaEducacao.Gestao.Application.Queries
{
    public class AlunoQueries : IAlunoQueries
    {
        private readonly IAlunoRepository _alunoRepository;

        public AlunoQueries(IAlunoRepository alunoRepository)
        {
            _alunoRepository = alunoRepository;
        }

        public async Task<MatriculasAlunoViewModel> ObterMatriculasAluno(Guid AlunoId)
        {
            var aluno = await _alunoRepository.ObterPorIdAsync(AlunoId);

            var result = new MatriculasAlunoViewModel();
            result.Nome = aluno.Nome;
            
            foreach (var matricula in aluno.Matriculas)
            {
                result.Matriculas.Add(new MatriculaViewModel()
                {
                    Curso = matricula.Curso.CursoNome!
                });
            }

            return result;
        }
    }
}
