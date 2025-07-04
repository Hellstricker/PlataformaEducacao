using MediatR;
using Moq;
using Moq.AutoMock;
using PlataformaEducacao.Cadastros.Application.Services;
using PlataformaEducacao.Cadastros.Application.ViewModels;
using PlataformaEducacao.Cadastros.Domain;
using PlataformaEducacao.Core.Data;
using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.Cadastros.Application.Tests.Cursos
{
    public class CursoApplicationServiceTests
    {
        private readonly AutoMocker _mocker;
        private readonly CursoApplicationService _cursoApplicationService;
        public CursoApplicationServiceTests()
        {
            _mocker = new AutoMocker();
            _cursoApplicationService = _mocker.CreateInstance<CursoApplicationService>();
        }

        [Fact(DisplayName = "Adicionar Curso Com Sucesso")]
        [Trait("Categoria", "Dominio Cadastro ApplicationService")]
        public async Task AdicionarCurso_CursoValido_DeveExecutarComSucesso()
        {
            // Arrange
            var cursoViewModel = new CursoViewModel()
            {
                Titulo = "Nome do Curso",
                Valor = 0.1M,
                Descricao = "Descricao Curso",
                CargaHoraria = 10
            };

            _mocker.GetMock<ICursoRepository>().Setup(repo=> repo.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

            // Act
            var result = await _cursoApplicationService.AdicionarCurso(cursoViewModel);

            // Assert
            Assert.True(result);
            _mocker.GetMock<ICursoRepository>().Verify(repo => repo.Adicionar(It.IsAny<Curso>()), Times.Once);
            _mocker.GetMock<ICursoRepository>().Verify(repo => repo.UnitOfWork.CommitAsync(), Times.Once);            
        }

        [Fact(DisplayName = "Adicionar Curso Inválido")]
        [Trait("Categoria", "Dominio Cadastro ApplicationService")]
        public async Task AdicionarCurso_CursoInvalido_DeveGerarNotificacao()
        {
            // Arrange
            var cursoViewModel = new CursoViewModel()
            {
                Titulo = "",
                Valor = -1,
                Descricao = "",
                CargaHoraria = 0
            };

            _mocker.GetMock<ICursoRepository>().Setup(repo => repo.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));

            // Act            
            var result = await _cursoApplicationService.AdicionarCurso(cursoViewModel);

            // Assert            
            Assert.False(result);
            _mocker.GetMock<IMediator>().Verify(not => not.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(4));
        }

        [Fact(DisplayName = "Adicionar Aula Valida")]
        [Trait("Categoria", "Dominio Cadastro ApplicationService")]
        public async Task AdicionarAula_AulaValida_DeveExecutarComSucesso()
        {
            // Arrange
            var curso = new Curso("Curso Teste", 100, new ConteudoProgramatico("Descricao Curso Teste", 20));
            var cursoId = curso.Id;
            var aulaViewModel = new AulaViewModel()
            {
                CursoId = cursoId,
                Titulo = "Titulo Aula",
                Conteudo = "Conteudo Aula Teste",
                Duracao = 10
            };

            _mocker.GetMock<ICursoRepository>().Setup(repo => repo.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));
            _mocker.GetMock<ICursoRepository>().Setup(repo => repo.ObterPorIdAsync(cursoId)).Returns(Task.FromResult(curso));

            // Act
            var result = await _cursoApplicationService.AdicionarAula(aulaViewModel);

            // Assert
            Assert.True(result);
            _mocker.GetMock<ICursoRepository>().Verify(repo => repo.Adicionar(It.IsAny<Aula>()), Times.Once);
            _mocker.GetMock<ICursoRepository>().Verify(repo => repo.UnitOfWork.CommitAsync(), Times.Once);
        }        

        [Fact(DisplayName = "Adicionar Aula Inválida")]
        [Trait("Categoria", "Dominio Cadastro ApplicationService")]
        public async Task AdicionarAula_AulaInvalida_DeveGerarNotificacao()
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
            var result = await _cursoApplicationService.AdicionarAula(aulaViewModel);

            // Assert
            Assert.False(result);
            _mocker.GetMock<IMediator>().Verify(not => not.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(4));    
        }        

        [Fact(DisplayName = "Adicionar Aula Com Curso Inexistente")]
        [Trait("Categoria", "Dominio Cadastro ApplicationService")]
        public async Task AdicionarAula_CursoInexistente_DeveGerarNotificacao()
        {
            // Arrange            
            var aulaViewModel = new AulaViewModel()
            {
                CursoId = Guid.NewGuid(),
                Titulo = "Titulo Aula",
                Conteudo = "Conteudo Aula Teste",
                Duracao = 10
            };

            // Act
            var result = await _cursoApplicationService.AdicionarAula(aulaViewModel);

            // Assert
            Assert.False(result);
            _mocker.GetMock<IMediator>().Verify(not => not.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }        

        [Fact(DisplayName = "Adicionar Aula Valida Duas Vezes")]
        [Trait("Categoria", "Dominio Cadastro ApplicationService")]
        public async Task AdicionarAula_AulaDuplicada_DeveGerarNotificacao()
        {
            // Arrange
            var curso = new Curso("Curso Teste", 100, new ConteudoProgramatico("Descricao Curso Teste", 20));
            var cursoId = curso.Id;
            var aulaViewModel = new AulaViewModel()
            {
                CursoId = cursoId,
                Titulo = "Titulo Aula",
                Conteudo = "Conteudo Aula Teste",
                Duracao = 10
            };

            _mocker.GetMock<ICursoRepository>().Setup(repo => repo.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));
            _mocker.GetMock<ICursoRepository>().Setup(repo => repo.ObterPorIdAsync(cursoId)).Returns(Task.FromResult(curso));
            await _cursoApplicationService.AdicionarAula(aulaViewModel);
            
            // Act
            var result = await _cursoApplicationService.AdicionarAula(aulaViewModel);

            // Assert
            Assert.False(result);
            _mocker.GetMock<IMediator>().Verify(not => not.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Aula Excede Carga Horaria")]
        [Trait("Categoria", "Dominio Cadastro ApplicationService")]
        public async Task AdicionarAula_CargahorariaExcedida_DeveGerarNotificacao()
        {
            // Arrange
            var cargaHorariaMaxima = 20;
            var curso = new Curso("Curso Teste", 100, new ConteudoProgramatico("Descricao Curso Teste", cargaHorariaMaxima));
            var cursoId = curso.Id;
            var aulaViewModel = new AulaViewModel()
            {
                CursoId = cursoId,
                Titulo = "Titulo Aula",
                Conteudo = "Conteudo Aula Teste",
                Duracao = 10
            };

            var aulaViewModel2 = new AulaViewModel()
            {
                CursoId = cursoId,
                Titulo = "Titulo Aula2",
                Conteudo = "Conteudo Aula Teste2",
                Duracao = cargaHorariaMaxima - aulaViewModel.Duracao + 1
            };

            _mocker.GetMock<ICursoRepository>().Setup(repo => repo.UnitOfWork.CommitAsync()).Returns(Task.FromResult(true));
            _mocker.GetMock<ICursoRepository>().Setup(repo => repo.ObterPorIdAsync(cursoId)).Returns(Task.FromResult(curso));
            await _cursoApplicationService.AdicionarAula(aulaViewModel);

            // Act
            var result = await _cursoApplicationService.AdicionarAula(aulaViewModel2);

            // Assert
            Assert.False(result);
            _mocker.GetMock<IMediator>().Verify(not => not.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }
    }
}
