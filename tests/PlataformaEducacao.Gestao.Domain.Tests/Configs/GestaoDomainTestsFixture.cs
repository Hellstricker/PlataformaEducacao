using Bogus;
using Bogus.DataSets;

namespace PlataformaEducacao.Gestao.Domain.Tests.Configs
{

    [CollectionDefinition(nameof(GestaoDomainCollection))]
    public class GestaoDomainCollection : 
        ICollectionFixture<GestaoDomainTestsFixture>
    {
    }
    public class GestaoDomainTestsFixture : IDisposable
    {  
        public Aluno GerarAlunoValido()
        {
            var gender = new Faker().PickRandom<Name.Gender>();
            
            return new Faker<Aluno>("pt_BR")
                .CustomInstantiator(f => new Aluno(f.Name.FullName(gender), f.Internet.Email()))
                .RuleFor(a => a.Email, (f, a) => {
                    var nomeParts = a.Nome.Split(' ');
                    return f.Internet.Email(nomeParts[0], nomeParts[nomeParts.Length - 1]).ToLower(); 
                });
        }

        public Aluno GerarAlunoInvaValido()
        {
            var gender = new Faker().PickRandom<Name.Gender>();
            return new Faker<Aluno>("pt_BR")
                .CustomInstantiator(f => new Aluno(f.Name.FullName(gender).First().ToString(), ""))
                .RuleFor(a => a.Email, (f, a) => a.Nome.Split(' ')[0].ToLower() + "@");
        }

        public Aluno GerarAlunoInvalidoDadosEmBranco()
        {
            return new Aluno("", "");
        }

        public void Dispose(){}

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
    }
}
