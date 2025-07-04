using PlataformaEducacao.Cadastros.Application.ViewModels;
using PlataformaEducacao.Core.ViewModelValidationHelpers;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.Cadastros.Application.Tests.Cursos
{
    public class CursoViewModelTests
    {
        [Fact(DisplayName = "CursoViewModel InValido")]
        [Trait("Categoria", "Dominio Cadastro ViewModels")]
        public void CursoViewModel_ViewModelEstaInValido_DeveGerarValidationResultErro()
        {
            // Arrange
            var cursoViewModel = new CursoViewModel()
            {
                Titulo = "",
                Valor = -1,
                Descricao = "",
                CargaHoraria = 0
            };

            // Act
            var validationResults = ViewModelTestsHelpers.ValidateModel(cursoViewModel);

            // Assert
            Assert.True(validationResults.Any(), "CursoViewModel deve conter erros de validação.");
            Assert.Equal(4, validationResults.Count);
        }

        [Fact(DisplayName = "CursoViewModel Valido")]
        [Trait("Categoria", "Dominio Cadastro ViewModels")]
        public void CursoViewModel_ViewModelEstaValido_NaoDeveGerarValidationResultErro()
        {
            // Arrange
            var cursoViewModel = new CursoViewModel()
            {
                Titulo = "Nome curso",
                Valor = 0.1M,
                Descricao = "Descricao Curso",
                CargaHoraria = 1
            };

            // Act
            var validationResults = ViewModelTestsHelpers.ValidateModel(cursoViewModel);

            // Assert
            Assert.False(validationResults.Any(), $"Erros da validação em CursoViewModel: {string.Join(", ", validationResults?.Select(x => x.ErrorMessage)!)}.");
        }
    }
}
