namespace PlataformaEducacao.Cadastros.Domain.Tests.Configs
{
    [CollectionDefinition(nameof(CursoCollection))]
    public class CursoCollection :
        ICollectionFixture<CursoTestsFixtures>, 
        ICollectionFixture<AulaTestsFixtures>
    { }

}
