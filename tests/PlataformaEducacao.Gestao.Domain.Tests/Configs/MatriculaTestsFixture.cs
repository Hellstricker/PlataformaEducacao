using Bogus;
using Bogus.DataSets;

namespace PlataformaEducacao.Gestao.Domain.Tests.Configs
{
    public class MatriculaTestsFixture : IDisposable
    {                  

        public Matricula GerarMatriculaValidaDuasAulas(Guid alunoId)
        {
            return Matricula.MariculaFactory.NovaMatriculaCorreta(alunoId, Guid.NewGuid(), "Curso Nome", 1000m, 2);
        }

        public Matricula GerarMatriculaValida(Guid alunoId)
        {
            return Matricula.MariculaFactory.NovaMatriculaCorreta(alunoId, Guid.NewGuid(), "Curso Nome", 1000m, 10);            
        }

        public Matricula GerarMatriculaValidaComStatusInvalido(Guid alunoId)
        {
            return Matricula.MariculaFactory.NovaMatriculaInCorreta(alunoId, Guid.NewGuid(), "Curso Nome", 1000m, 10);
        }

        public Matricula GerarMatriculaInvalidaVazia(Guid alunoId)
        {
            return Matricula.MariculaFactory.NovaMatriculaInCorreta(alunoId, Guid.Empty, string.Empty, 0, 0);
        }

        public void Dispose() { }
    }
}
