using MediatR;
using Moq;
using Moq.AutoMock;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.Gestao.Application.Commands;
using PlataformaEducacao.Gestao.Application.Commands.Validations;
using PlataformaEducacao.Gestao.Application.Tests.Configs;
using PlataformaEducacao.Gestao.Domain;
using PlataformaEducacao.Gestao.Domain.Tests.Configs;

namespace PlataformaEducacao.Gestao.Application.Tests
{
    [Collection(nameof(GestaoApplicationCollection))]
    public class AlunoCommandHandlerTests 
    {
        private readonly GestaoApplicationTestsFixture _fixture;
        private readonly AutoMocker _mocker;
        private readonly AlunoCommandHandler _handler;
        private readonly GestaoDomainTestsFixture _alunoTestsFixture;

        public AlunoCommandHandlerTests(GestaoApplicationTestsFixture fixture, GestaoDomainTestsFixture alunoTestsFixture)
        {
            _fixture = fixture;
            _alunoTestsFixture = alunoTestsFixture;
            _mocker = new AutoMocker();
            _handler = _mocker.CreateInstance<AlunoCommandHandler>();
        }

        [Fact(DisplayName = "Cdastrar Aluno Sucesso")]
        [Trait("Categoria", "Dominio Gestao CommandHandler")]
        public async Task AlunoCommandHandler_CadastrarAlunoCommand_DeveRetornarSucesso()
        {
            // Arrange
            var command = _fixture.GerarCadastrarAlunoCommandValido();  
            _mocker.GetMock<IAlunoRepository>().Setup(repo => repo.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));
            
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.True(result);
            Assert.Equal(command.ValidationResult.IsValid, result);            
            _mocker.GetMock<IAlunoRepository>().Verify(r => r.Adicionar(It.IsAny<Aluno>()), Times.Once);            
        }

        [Fact(DisplayName = "Matricular Aluno Sucesso")]
        [Trait("Categoria", "Dominio Gestao CommandHandler")]
        public async Task AlunoCommandHandler_MatricularAlunoCommand_DeveRetornarSucesso()
        {
            // Arrange
            var aluno = _alunoTestsFixture.GerarAlunoValido();

            var command = _fixture.GerarMatricularAlunoCommandValido(aluno.Id);
            _mocker.GetMock<IAlunoRepository>().Setup(repo => repo.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));
            _mocker.GetMock<IAlunoRepository>().Setup(repo => repo.ObterPorIdAsync(aluno.Id)).ReturnsAsync(aluno);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal(command.ValidationResult.IsValid, result);
            _mocker.GetMock<IAlunoRepository>().Verify(r => r.Adicionar(It.IsAny<Matricula>()), Times.Once);
        }

        [Fact(DisplayName = "Matricular Aluno Mesmo curso")]
        [Trait("Categoria", "Dominio Gestao CommandHandler")]
        public async Task AlunoCommandHandler_MatricularAlunoCommand_DeveRetornarMensagemErro()
        {
            // Arrange
            var aluno = _alunoTestsFixture.GerarAlunoValido();
            var command = _fixture.GerarMatricularAlunoCommandValido(aluno.Id);            
            _mocker.GetMock<IAlunoRepository>().Setup(repo => repo.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));
            _mocker.GetMock<IAlunoRepository>().Setup(repo => repo.ObterPorIdAsync(aluno.Id)).ReturnsAsync(aluno);
            await _handler.Handle(command, CancellationToken.None);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mocker.GetMock<IMediatorHandler>().Verify(not => not.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Exactly(1));
        }

        [Fact(DisplayName = "Aluno Pagar Matricula Status Em Andamento")]
        [Trait("Categoria", "Dominio Gestao CommandHandler")]
        public async Task AlunoCommandHandler_AlunoPagarMatriculaCommand_NaoDeveRetornarMensagemErro()
        {
            // Arrange
            var aluno = _alunoTestsFixture.GerarAlunoValido();
            var command = _fixture.GerarMatricularAlunoCommandValido(aluno.Id);
            _mocker.GetMock<IAlunoRepository>().Setup(repo => repo.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));
            _mocker.GetMock<IAlunoRepository>().Setup(repo => repo.ObterPorIdAsync(aluno.Id)).ReturnsAsync(aluno);
            await _handler.Handle(command, CancellationToken.None);
            var commandPagar = new AlunoPagarMatriculaCommand(command.AlunoId, command.CursoId);

            // Act
            var result = await _handler.Handle(commandPagar, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IMediatorHandler>().Verify(not => not.PublicarNotificacao(It.IsAny<DomainNotification>()), Times.Never);
        }
    }
}
