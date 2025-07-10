using Bogus;
using Bogus.DataSets;
using PlataformaEducacao.Gestao.Application.Commands;
using PlataformaEducacao.Gestao.Domain;
using PlataformaEducacao.Gestao.Domain.Tests.Configs;

namespace PlataformaEducacao.Gestao.Application.Tests.Configs
{

    [CollectionDefinition(nameof(GestaoApplicationCollection))]
    public class GestaoApplicationCollection :         
        ICollectionFixture<GestaoApplicationTestsFixture>,
        ICollectionFixture<AlunoTestsFixture>
    {
    }
    public class GestaoApplicationTestsFixture : IDisposable
    {
        private readonly string senhaForte = "SenhaForte123!";

        public CadastrarAlunoCommand GerarCadastrarAlunoCommandValido()
        {            
            var f = new Faker("pt_BR");
            var gender = f.PickRandom<Name.Gender>();           

            
            return new Faker<CadastrarAlunoCommand>("pt_BR")
                .CustomInstantiator(f => new CadastrarAlunoCommand(f.Name.FullName(gender), f.Internet.Email(), senhaForte, senhaForte))
                .RuleFor(a => a.Email, (f, a) => {
                    var nomeParts = a.Nome.Split(' ');
                    return f.Internet.Email(nomeParts[0], nomeParts[nomeParts.Length - 1]).ToLower();
                });
        }
        public CadastrarAlunoCommand GerarCadastrarAlunoCommandInvalido()
        {
            var gender = new Faker().PickRandom<Name.Gender>();
            var senha = new Faker().Internet.Password(4, true, @"\w\d", "!@#$%^&*()_+[]{}|;:,.<>?");
            return new Faker<CadastrarAlunoCommand>("pt_BR")
                .CustomInstantiator(f => new CadastrarAlunoCommand(f.Name.FullName(gender).First().ToString(), "", senha, senha))
                .RuleFor(a => a.Email, (f, a) => a.Nome.Split(' ')[0].ToLower() + "@");
        }

        public CadastrarAlunoCommand GerarCadastrarAlunoCommandInvalido(Aluno aluno)
        {
            var f = new Faker("pt_BR");
            var gender = f.PickRandom<Name.Gender>();


            return new Faker<CadastrarAlunoCommand>("pt_BR")
                .CustomInstantiator(f => new CadastrarAlunoCommand(aluno.Nome, aluno.Email, senhaForte, senhaForte))
                .RuleFor(a => a.Email, (f, a) => {
                    var nomeParts = a.Nome.Split(' ');
                    return f.Internet.Email(nomeParts[0], nomeParts[nomeParts.Length - 1]).ToLower();
                });
        }

        public CadastrarAlunoCommand GerarCadastrarAlunoCommandDadosEmBranco()
        {
            return new CadastrarAlunoCommand("", "", "", "");
        }


        public MatricularAlunoCommand GerarMatricularAlunoCommandValido()
        {
            return new MatricularAlunoCommand(Guid.NewGuid(), Guid.NewGuid(), "Nome Curso", 1000m, 10);
        }

        public MatricularAlunoCommand GerarMatricularAlunoCommandValido(Guid alunoId)
        {
            var command = GerarMatricularAlunoCommandValido();
            return new MatricularAlunoCommand(alunoId, command.CursoId, command.CursoNome, command.CursoValor, command.CursoTotalAulas);
        }

        public MatricularAlunoCommand GerarMatricularAlunoCommandValidoComQuantidadeDeAulas(Guid alunoId, int totalAulas)
        {
            var command = GerarMatricularAlunoCommandValido();
            return new MatricularAlunoCommand(alunoId, command.CursoId, command.CursoNome, command.CursoValor, totalAulas);
        }


        public void Dispose(){}
    }
}
