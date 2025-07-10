namespace PlataformaEducacao.Gestao.Domain.Tests.Configs
{
    [CollectionDefinition(nameof(AlunoCollection))]
    public class AlunoCollection : 
        ICollectionFixture<AlunoTestsFixture>,
        ICollectionFixture<MatriculaTestsFixture>
    {
    }
}
