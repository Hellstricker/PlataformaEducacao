using PlataformaEducacao.Gestao.Application.Commands;
using PlataformaEducacao.Gestao.Application.Commands.Validations;
using PlataformaEducacao.Gestao.Application.Tests.Configs;
using PlataformaEducacao.Gestao.Domain;


namespace PlataformaEducacao.Gestao.Application.Tests
{
    [Collection(nameof(GestaoApplicationCollection))]
    public class GestaoApplicationCommandsTests
    {
        private readonly GestaoApplicationTestsFixture _fixture;

        public GestaoApplicationCommandsTests(GestaoApplicationTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "CadastrarAlunoCommand Valido")]
        [Trait("Categoria", "Dominio Gestao Commands")]
        public void CadastrarAlunoCommand_ComandoValido_NaoDeveRetornarErro()
        {
            // Arrange
            var command = _fixture.GerarCadastrarAlunoCommandValido();
            // Act
            var result = command.EhValido();
            // Assert
            Assert.True(result);
            Assert.Empty(command.ValidationResult.Errors);            
        }

        [Fact(DisplayName = "CadastrarAlunoCommand InValido")]
        [Trait("Categoria", "Dominio Gestao Commands")]
        public void CadastrarAlunoCommand_ComandoInValido_DeveRetornarErro()
        {
            // Arrange
            var command = _fixture.GerarCadastrarAlunoCommandInvalido();
            // Act
            var result = command.EhValido();
            // Assert
            Assert.False(result);
            Assert.NotEmpty(command.ValidationResult.Errors);
            Assert.Equal(2, command.ValidationResult.Errors.Count);            
            Assert.Contains(command.ValidationResult.Errors, e => e.ErrorMessage == CadastrarAlunoCommandValidation.NomeTamanhoMensagemErro);            
            Assert.Contains(command.ValidationResult.Errors, e => e.ErrorMessage == CadastrarAlunoCommandValidation.EmailFormatoMensagemErro);
        }

        [Fact(DisplayName = "CadastrarAlunoCommand Em Branco")]
        [Trait("Categoria", "Dominio Gestao Commands")]
        public void CadastrarAlunoCommand_ComandoEmBranco_DeveRetornarErro()
        {
            // Arrange
            var command = _fixture.GerarCadastrarAlunoCommandDadosEmBranco();
            // Act
            var result = command.EhValido();
            // Assert
            Assert.False(result);
            Assert.NotEmpty(command.ValidationResult.Errors);
            Assert.Equal(4, command.ValidationResult.Errors.Count);
            Assert.Contains(command.ValidationResult.Errors, e => e.ErrorMessage == CadastrarAlunoCommandValidation.NomeVazioMensagemErro);
            Assert.Contains(command.ValidationResult.Errors, e => e.ErrorMessage == CadastrarAlunoCommandValidation.NomeTamanhoMensagemErro);
            Assert.Contains(command.ValidationResult.Errors, e => e.ErrorMessage == CadastrarAlunoCommandValidation.EmailVazioMensagemErro);
            Assert.Contains(command.ValidationResult.Errors, e => e.ErrorMessage == CadastrarAlunoCommandValidation.EmailFormatoMensagemErro);
        }


        [Fact(DisplayName = "MaricularAlunoCommand Valido")]
        [Trait("Categoria", "Dominio Gestao Commands")]
        public void MaricularAlunoCommand_ComandoValido_NaoDeveRetornarErro()
        {
            // Arrange
            var command = _fixture.GerarMatricularAlunoCommandValido();            

            // Act
            var result = command.EhValido();
            // Assert
            Assert.True(result);
            Assert.Empty(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "AlunoPagarMatriculaCommand Valido")]
        [Trait("Categoria", "Dominio Gestao Commands")]
        public void AlunoPagarMatriculaCommand_ComandoValido_NaoDeveRetornarErro()
        {
            // Arrange
            var command = new AlunoPagarMatriculaCommand(Guid.NewGuid(), Guid.NewGuid(), "Maria Joaquina", "1256548748955412", "12/29","123");

            // Act
            var result = command.EhValido();
            // Assert
            Assert.True(result);
            Assert.Empty(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "AlunoFinalizarAulaCommand Valido")]
        [Trait("Categoria", "Dominio Gestao Commands")]
        public void AlunoFinalizarAulaCommand_ComandoValido_NaoDeveRetornarErro()
        {
            // Arrange
            var command = new AlunoFinalizarAulaCommand(Guid.NewGuid(),Guid.NewGuid(),Guid.NewGuid());

            // Act            
            var result = command.EhValido();
            
            // Assert
            Assert.True(result);
            Assert.Empty(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "AlunoFinalizarMatriculaCommand Valido")]
        [Trait("Categoria", "Dominio Gestao Commands")]
        public void AlunoFinalizarMatriculaCommand_ComandoValido_NaoDeveRetornarErro()
        {
            // Arrange
            var command = new AlunoFinalizarMatriculaCommand(Guid.NewGuid(),Guid.NewGuid());

            // Act            
            var result = command.EhValido();

            // Assert
            Assert.True(result);
            Assert.Empty(command.ValidationResult.Errors);
        }

        [Fact(DisplayName = "AlunoGerarCertificadoCommand Valido")]
        [Trait("Categoria", "Dominio Gestao Commands")]
        public void AlunoGerarCertificadoCommand_ComandoValido_NaoDeveRetornarErro()
        {
            // Arrange
            var command = new AlunoGerarCertificadoCommand(Guid.NewGuid(), Guid.NewGuid());            

            // Act            
            var result = command.EhValido();

            // Assert
            Assert.True(result);
            Assert.Empty(command.ValidationResult.Errors);
        }
    }
}
