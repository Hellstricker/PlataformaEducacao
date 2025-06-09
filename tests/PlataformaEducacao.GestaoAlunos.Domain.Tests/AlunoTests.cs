using PlataformaEducacao.Core.DomainObjects;
using System;

namespace PlataformaEducacao.GestaoAlunos.Domain.Tests
{
    public class AlunoTests
    {
        [Fact(DisplayName = "Criar Aluno Sem Nome")]
        [Trait("Aluno", "1 Aluno Tests")]
        public void Curso_Validar_ValidacaoDeveRetornarAlunoSemNomeException()
        {
            // Arrange & Act
            Action act = () => { var aluno = new Aluno(Guid.NewGuid(),string.Empty); };
            // Assert
            Assert.Equal("O nome do(a) aluno(a) não pode ser vazio", Assert.Throws<DomainException>(act).Message);
        }

        [Fact(DisplayName = "Criar Aluno Sem Id")]
        [Trait("Aluno", "1 Aluno Tests")]
        public void Curso_Validar_ValidacaoDeveRetornarAlunoSemIdException()
        {
            // Arrange & Act
            Action act = () => { var aluno = new Aluno(Guid.Empty, "Teste"); };
            // Assert
            Assert.Equal("O Id do(a) aluno(a) não pode ser vazio", Assert.Throws<DomainException>(act).Message);
        }

        [Fact(DisplayName = "Matricular Aluno com Sucesso")]
        [Trait("Aluno", "1 Aluno Tests")]
        public void Curso_Validar_ValidacaoDeveRetornarMatriculaComSucesso()
        {
            //Arrange
            var aluno = new Aluno(Guid.NewGuid(), "Teste");
            var matricula = new Matricula(aluno.Id, Guid.NewGuid(), "CursoCurso de Teste");            
            // Act            
            var exception = Record.Exception(() => aluno.Matricular(matricula));            
            // Assert                      
            Assert.Null(exception);
        }


        [Fact(DisplayName = "Matricular Aluno Em Curso Já Matriculado")]
        [Trait("Aluno", "1 Aluno Tests")]
        public void Curso_Validar_ValidacaoDeveRetornarAlunoJaMatriculadoException()
        {
            //Arrange
            var aluno = new Aluno(Guid.NewGuid(), "Teste");
            var cursoId = Guid.NewGuid();
            var matricula = new Matricula(aluno.Id, cursoId, "CursoCurso de Teste");
            var matricula2 = new Matricula(aluno.Id, cursoId, "CursoCurso de Teste");
            // Act            
            aluno.Matricular(matricula);
            Action act = () => { aluno.Matricular(matricula); };
            
            // Assert                      
            Assert.Equal("Aluno já está matriculado neste curso.", Assert.Throws<DomainException>(act).Message);
        }
    }
}