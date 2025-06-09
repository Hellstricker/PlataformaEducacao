using AutoMapper;
using Moq;
using Moq.AutoMock;
using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.GestaoCursos.Application.Dtos;
using PlataformaEducacao.GestaoCursos.Application.Services;
using PlataformaEducacao.GestaoCursos.Domain;
using PlataformaEducacao.GestaoCursos.Domain.DomainServices;
using PlataformaEducacao.GestaoCursos.Domain.Interfaces;
namespace PlataformaEducacao.GestaoCursos.Domain.Tests
{
    public class CursoTests
    {
        [Fact(DisplayName = "Criar Curso Sem Valor Exception")]
        [Trait("Curso", "1 Curso Tests")]
        public void Curso_Validar_ValidacaoDeveRetornarCursoSemValorException()
        {
            // Arrange & Act
            Action act = () => { var curso = new Curso("Curso", 0, new ConteudoProgramatico("Objetivo", "Conteudo")); };
            // Assert
            Assert.Equal("O valor do curso deve ser maior que zero", Assert.Throws<DomainException>(act).Message);
        }

        [Fact(DisplayName = "Criar Curso Sem Nome Exception")]
        [Trait("Curso", "1 Curso Tests")]
        public void Curso_Validar_ValidacaoDeveRetornarCursoSemNomeException()
        {
            // Arrange & Act
            Action act = () => { var curso = new Curso(string.Empty, 10, new ConteudoProgramatico("Objetivo", "Conteudo")); };
            // Assert
            Assert.Equal("Informe o nome do curso", Assert.Throws<DomainException>(act).Message);
        }

        [Fact(DisplayName = "Criar Curso Sem Objetivo Exception")]
        [Trait("Curso", "1 Curso Tests")]
        public void Curso_Validar_ValidacaoDeveRetornarCursoSemObjetivoException()
        {
            // Arrange & Act
            Action act = () => { var curso = new Curso("Nome", 10, new ConteudoProgramatico(string.Empty, "Conteudo")); };
            // Assert
            Assert.Equal("Informe o objetivo do curso", Assert.Throws<DomainException>(act).Message);
        }

        [Fact(DisplayName = "Criar Curso Sem Conteudo Exception")]
        [Trait("Curso", "1 Curso Tests")]
        public void Curso_Validar_ValidacaoDeveRetornarCursoSemConteudoException()
        {
            // Arrange & Act
            Action act = () => { var curso = new Curso("Nome",10, new ConteudoProgramatico("Objetivo", string.Empty)); };
            // Assert
            Assert.Equal("Informe o conteúdo do curso", Assert.Throws<DomainException>(act).Message);
        }

        [Fact(DisplayName = "Criar Curso Valido")]
        [Trait("Curso", "1 Curso Tests")]
        public void Curso_Validar_ValidacaoNaoDeveRetornarException()
        {
            // Arrange & Act
            var exception = Record.Exception(() => new Curso("Nome", 10, new ConteudoProgramatico("Objetivo", "Conteudo")));
            // Assert            
            Assert.Null(exception);
        }
    }

    public class CursoDomainServiceTests
    {
        [Fact(DisplayName = "Adicionar Curso Com Sucesso")]
        [Trait("Curso", "2 Curso Domain Service Tests")]
        public async void Curso_Adicionar_AdicionarCursoComSucesso()
        {
            // Arrange            
            var curso = new Curso("Nome", 10, new ConteudoProgramatico("Objetivo", "Conteudo"));

            var mocker = new AutoMocker();
            var cursoService = mocker.CreateInstance<GestaoCursosDomainService>();
           
            mocker.GetMock<ICursoRepository>()
                .Setup(x => x.UnitOfWork.CommitAsync())
                .Returns(Task.FromResult(true));

            // Act
            var exception = await Record.ExceptionAsync(() => cursoService.CadastrarCurso(curso));

            // Assert
            Assert.Null(exception);
            mocker.GetMock<ICursoRepository>().Verify(x => x.Adicionar(It.IsAny<Curso>()), Times.Once);
        }
    }
}