using PlataformaEducacao.Cadastros.Application.ViewModels;
using PlataformaEducacao.Core.ViewModelValidationHelpers;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.Cadastros.Application.Tests.Cursos
{
    public class AulaViewModelTests
    {
        [Fact(DisplayName = "AulaViewModel InValido")]
        [Trait("Categoria", "Dominio Cadastro ViewModels")]
        public void AulaViewModel_ViewModelInvalida_DeveGerarValidationResultErro()
        {
            // Arrange
            var aulaViewModel = new AulaViewModel()
            {
                CursoId = Guid.Empty,
                Titulo = "",                
                Conteudo = "",
                Duracao = 0
            };

            // Act
            var validationResults = ViewModelTestsHelpers.ValidateModel(aulaViewModel);

            // Assert
            Assert.True(validationResults.Any(), "AulaViewModel deve conter erros de validação.");
            Assert.Equal(4, validationResults.Count);
        }

        [Fact(DisplayName = "AulaViewModel Valido")]
        [Trait("Categoria", "Dominio Cadastro ViewModels")]
        public void AulaViewModel_ViewModelValida_NaoDeveGerarValidationResultErro()
        {
            // Arrange
            var aulaViewModel = new AulaViewModel()
            {
                CursoId = Guid.NewGuid(),
                Titulo = "Titulo aula",
                Conteudo = "Conteudo da aula",
                Duracao = 10
            };

            // Act
            var validationResults = ViewModelTestsHelpers.ValidateModel(aulaViewModel);

            // Assert
            Assert.False(validationResults.Any(), $"Erros da validação em AulaViewModel: {string.Join(", ", validationResults?.Select(x => x.ErrorMessage)!)}.");            
        }
    }
}
