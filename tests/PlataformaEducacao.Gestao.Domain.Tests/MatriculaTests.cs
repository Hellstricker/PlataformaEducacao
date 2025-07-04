using Castle.Components.DictionaryAdapter.Xml;
using PlataformaEducacao.Gestao.Domain.Tests.Configs;

namespace PlataformaEducacao.Gestao.Domain.Tests
{
    [Collection(nameof(GestaoDomainCollection))]
    public class MatriculaTests
    {
        private readonly GestaoDomainTestsFixture _fixtures;

        public MatriculaTests(GestaoDomainTestsFixture fixtures)
        {
            _fixtures = fixtures;
        }

        [Fact(DisplayName = "Matricula Válida")]
        [Trait("Categoria", "Domínio Gestao Entidade Matricula")]
        public void Matricula_EntidadeValida_DevePassarNaValidacao()
        {
            // Arrange
            var matricula = _fixtures.GerarMatriculaValida(Guid.NewGuid());

            // Act
            var result = matricula.EhValido();

            // Assert
            Assert.True(result);
            Assert.Empty(matricula.ValidationResult.Errors);
        }
    }
}
