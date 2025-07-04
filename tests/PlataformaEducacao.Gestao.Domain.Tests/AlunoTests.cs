using Moq;
using Moq.AutoMock;
using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Gestao.Domain.Tests.Configs;
using PlataformaEducacao.Gestao.Domain.Validations;

namespace PlataformaEducacao.Gestao.Domain.Tests
{
    [Collection(nameof(GestaoDomainCollection))]
    public class AlunoTests
    {

        private readonly GestaoDomainTestsFixture _fixtures;
        private readonly AutoMocker _mocker;

        public AlunoTests(GestaoDomainTestsFixture fixtures)
        {
            _fixtures = fixtures;
            _mocker = new AutoMocker();
        }

        [Fact(DisplayName = "Aluno Valido")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_EntidadeValida_DevePassarNaValidacao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            // Act
            var result = aluno.EhValido();
            // Assert
            Assert.True(result, "O aluno deve ser considerado válido.");
            Assert.Empty(aluno.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Aluno Inválido")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_EntidadeInValida_NaoDevePassarNaValidacao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoInvaValido();
            // Act
            var result = aluno.EhValido();
            // Assert
            Assert.False(result, "O aluno deve ser considerado inválido.");
            Assert.NotEmpty(aluno.ValidationResult.Errors);
            Assert.Equal(2, aluno.ValidationResult.Errors.Count);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.NomeTamanhoMensagemErro);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.EmailFormatoMensagemErro);
        }

        [Fact(DisplayName = "Aluno Inválido Não Preenchido")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_EntidadeNaoPreenchida_NaoDevePassarNaValidacao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoInvalidoDadosEmBranco();
            // Act
            var result = aluno.EhValido();
            // Assert
            Assert.False(result, "O aluno deve ser considerado inválido.");
            Assert.NotEmpty(aluno.ValidationResult.Errors);
            Assert.Equal(4, aluno.ValidationResult.Errors.Count);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.NomeVazioMensagemErro);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.NomeTamanhoMensagemErro);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.EmailVazioMensagemErro);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.EmailFormatoMensagemErro);
        }

        [Fact(DisplayName = "Aluno Matricular Com Sucesso")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_RealizarMatriculaValida_NaoDeveRetornarErros()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValida(aluno.Id);
            // Act
            aluno.Matricular(matricula);

            // Assert
            Assert.Contains(aluno.Matriculas, m => m.Id == matricula.Id);
        }

        [Fact(DisplayName = "Aluno Realizar Matricula Mesmo Curso")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_RealizarMatriculaMesmoCurso_DeveGerarExcecao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValida(aluno.Id);
            aluno.Matricular(matricula);

            // Act && Assert
            var ex = Assert.Throws<DomainException>(() => aluno.Matricular(matricula));
            Assert.Contains(Aluno.AlunoJaMatriculado, ex.Message);
        }

        [Fact(DisplayName = "Aluno Pagar Matricula Status Invalido")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_PagarMatriculaStatusInvalido_DeveGerarExcecao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValidaComStatusInvalido(aluno.Id);            

            // Act && Assert
            var ex = Assert.Throws<DomainException>(() => matricula.Pagar());
            Assert.Contains(Matricula.MatriculaNaoEstaPendente, ex.Message);
        }

        [Fact(DisplayName = "Aluno Pagar Matricula Status Correto")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_PagarMatriculaStatusCorreto_NaoDeveGerarExcecao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValida(aluno.Id);
            aluno.Matricular(matricula);            

            // Act
             matricula.Pagar();

            //Assert
            Assert.Equal(StatusMatriculaEnum.EM_ANDAMENTO, matricula.Status);
        }
    }
}
