using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlataformaEducacao.Cadastros.Data;
using PlataformaEducacao.Core.Messages.IntegrationEvents;
using PlataformaEducacao.Gestao.Application.Queries.ViewModels;
using PlataformaEducacao.Gestao.Application.Tests.Configs;
using PlataformaEducacao.Gestao.Data;
using PlataformaEducacao.Gestao.Domain;
using PlataformaEducacao.Gestao.Domain.Tests.Configs;
using PlataformaEducacao.Pagamentos.Business;
using PlataformaEducacao.WebApps.Tests.Configs;
using PlataformaEducacao.WebApps.WebApi.ViewModels;
using System.Net.Http.Json;

namespace PlataformaEducacao.WebApps.Tests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class GestaoTests
    {
        private readonly IntegrationTestsFisxture<Program> _fixture;
        private readonly GestaoApplicationTestsFixture _alunoTestsFixture;
        private readonly AlunoTestsFixture _gestaoDomainTestsFixture;


        private readonly Aluno _alunoValido;
        private readonly CadastrarAlunoViewModel _cadastrarAlunoViewModelValido;

        public GestaoTests(IntegrationTestsFisxture<Program> fisxture, GestaoApplicationTestsFixture alunoTestsFixture, AlunoTestsFixture gestaoDomainTestsFixture)
        {
            _fixture = fisxture;            
            _alunoTestsFixture = alunoTestsFixture;
            _gestaoDomainTestsFixture = gestaoDomainTestsFixture;

            _alunoValido = _gestaoDomainTestsFixture.GerarAlunoValido();
            _cadastrarAlunoViewModelValido = ObterCadastrarAlunoViewModelValido();
        }

        [Fact(DisplayName = "Realizar Cadastro Aluno Sucesso")]
        [Trait("Categoria", "Integracao API - Alunos")]
        public async Task CadastrarAluno_QualquerPesssoaCadastraAluno_DeveRetornarComSucesso()
        {
            // Arrange
            var gestaoContext = _fixture.ObterServico<GestaoContext>();
            var userManager = _fixture.ObterServico<UserManager<IdentityUser<Guid>>>();            

            // Act
            var cadastrarAlunoResponse = await _fixture.ChamaEndpointCadastrarAluno(_cadastrarAlunoViewModelValido);

            // Assert                        
            var postCadastrarAlunoResponseContent = await cadastrarAlunoResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();
            Assert.True(postCadastrarAlunoResponseContent!.Success, postCadastrarAlunoResponseContent.Errors != null ? string.Join(", ", postCadastrarAlunoResponseContent.Errors!.Select(x => x)) : null);            
            Assert.True(gestaoContext.Alunos.Any());
            var alunoCadastrado = gestaoContext.Alunos.FirstOrDefault(x => x.Email == _cadastrarAlunoViewModelValido.Email);
            Assert.NotNull(alunoCadastrado);
            var usuarioAluno = await userManager.FindByEmailAsync(alunoCadastrado.Email);
            Assert.NotNull(usuarioAluno);
            Assert.Equal(alunoCadastrado.Id, usuarioAluno.Id);
            
        }


        [Fact(DisplayName = "Realizar Matricula Aluno Sucesso")]
        [Trait("Categoria", "Integracao API - Alunos")]
        public async Task MatricularAluno_AlunoCadastradoRealizaMatricula_DeveRetornarComSucesso()
        {
            // Arrange
            await _fixture.CadastrarLogarAdministrador();
            var cadastrarCurso = await _fixture.ChamaEndpointCadastraCursoValido();
            cadastrarCurso.EnsureSuccessStatusCode();
            var gestaoContext = _fixture.ObterServico<GestaoContext>();
            var cursosContext = _fixture.ObterServico<CursoContext>();            
            var cursoId = cursosContext.Cursos.First().Id;            
            await _fixture.CadastrarLogarAluno(_cadastrarAlunoViewModelValido);

            var matriculaViewModel = new MatricularAlunoViewModel
            {                
                CursoId = cursoId,
                CursoNome = "Curso Teste",
                CursoValor = 1000m,
                CursoTotalAulas = 10
            };

            // Act
            var matricularAlunoResponse = await _fixture.ChamaEndpointMatricularAluno(_fixture.UsuarioLogado.Id, matriculaViewModel);

            // Assert
            matricularAlunoResponse.EnsureSuccessStatusCode();
            var postMatricularAlunoResponseContent = await matricularAlunoResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();            
            Assert.True(postMatricularAlunoResponseContent!.Success, postMatricularAlunoResponseContent.Errors != null ? string.Join(", ", postMatricularAlunoResponseContent.Errors!.Select(x => x)) : null);
            Assert.True(gestaoContext.Matriculas.Any());
            Assert.True(gestaoContext.Matriculas.Any(x => x.AlunoId == _fixture.UsuarioLogado.Id && x.Curso.CursoId == matriculaViewModel.CursoId), "Matricula não encontrada para o aluno e curso especificados.");
        }


        [Fact(DisplayName = "Realizar Pagamento Matricula Aluno Sucesso")]
        [Trait("Categoria", "Integracao API - Alunos")]
        public async Task PagarMatriculaAluno_AlunoCadastradoRealizaPagamentoMatricula_DeveRetornarComSucesso()
        {
            // Arrange
            await _fixture.CadastrarLogarAdministrador();
            var cadastrarCurso = await _fixture.ChamaEndpointCadastraCursoValido();
            cadastrarCurso.EnsureSuccessStatusCode();
            var gestaoContext = _fixture.ObterServico<GestaoContext>();
            var cursosContext = _fixture.ObterServico<CursoContext>();
            var cursoId = cursosContext.Cursos.First().Id;
            await _fixture.CadastrarLogarAluno(_cadastrarAlunoViewModelValido);

            var matriculaViewModel = new MatricularAlunoViewModel
            {
                CursoId = cursoId,
                CursoNome = "Curso Teste",
                CursoValor = 1000m,
                CursoTotalAulas = 10
            };
            var matricularAlunoResponse = await _fixture.ChamaEndpointMatricularAluno(_fixture.UsuarioLogado.Id, matriculaViewModel);
            matricularAlunoResponse.EnsureSuccessStatusCode();

            var pagamentoModel = new PagamentoMatriculaViewModel
            {
                CursoId = cursoId,
                NomeCartao = "Nome do Cartão",
                NumeroCartao = "1234567890123456",
                MesAnoExpiracao = "12/25",
                Ccv = "123"
            };

            // Act
            var pagarMatriculaAlunoResponse = await _fixture.ChamaEndpointPagarMatriculaAluno(_fixture.UsuarioLogado.Id, pagamentoModel);
            while (gestaoContext.Matriculas.Any(x => x.AlunoId == _fixture.UsuarioLogado.Id && x.Curso.CursoId == matriculaViewModel.CursoId && x.Status == StatusMatriculaEnum.PENDENTE_PAGAMENTO))
            {                
                pagarMatriculaAlunoResponse = await _fixture.ChamaEndpointPagarMatriculaAluno(_fixture.UsuarioLogado.Id, pagamentoModel);                
            }


            // Assert
            pagarMatriculaAlunoResponse.EnsureSuccessStatusCode();
            var pagarMatriculaAlunoResponseContent = await pagarMatriculaAlunoResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();            
            Assert.NotNull(pagarMatriculaAlunoResponseContent);
            Assert.True(gestaoContext.Matriculas.Any());
            Assert.True((pagarMatriculaAlunoResponseContent.Success && gestaoContext.Matriculas.Any(x => x.AlunoId == _fixture.UsuarioLogado.Id && x.Curso.CursoId == matriculaViewModel.CursoId && x.Status == StatusMatriculaEnum.EM_ANDAMENTO)));
        }       


        [Fact(DisplayName = "Finalizar Aula com Sucesso")]
        [Trait("Categoria", "Integracao API - Alunos")]
        public async Task AlunoFinalizarAula_AlunoCadastradoFinalizaAulaEmCursoMatriculado_DeveRetornarComSucesso()
        {
            // Arrange
            await _fixture.CadastrarLogarAdministrador();
            var cadastrarCurso = await _fixture.ChamaEndpointCadastraCursoValido();
            cadastrarCurso.EnsureSuccessStatusCode();
            var gestaoContext = _fixture.ObterServico<GestaoContext>();
            var cursosContext = _fixture.ObterServico<CursoContext>();
            var cursoId = cursosContext.Cursos.First().Id;
            await _fixture.CadastrarLogarAluno(_cadastrarAlunoViewModelValido);

            var matriculaViewModel = new MatricularAlunoViewModel
            {
                CursoId = cursoId,
                CursoNome = "Curso Teste",
                CursoValor = 1000m,
                CursoTotalAulas = 10
            };
            var matricularAlunoResponse = await _fixture.ChamaEndpointMatricularAluno(_fixture.UsuarioLogado.Id, matriculaViewModel);
            matricularAlunoResponse.EnsureSuccessStatusCode();

            var pagamentoModel = new PagamentoMatriculaViewModel
            {
                CursoId = cursoId,
                NomeCartao = "Nome do Cartão",
                NumeroCartao = "1234567890123456",
                MesAnoExpiracao = "12/25",
                Ccv = "123"
            };

            
            var pagarMatriculaAlunoResponse = await _fixture.ChamaEndpointPagarMatriculaAluno(_fixture.UsuarioLogado.Id, pagamentoModel);
            while (gestaoContext.Matriculas.Any(x => x.AlunoId == _fixture.UsuarioLogado.Id && x.Curso.CursoId == matriculaViewModel.CursoId && x.Status == StatusMatriculaEnum.PENDENTE_PAGAMENTO))
            {

                pagarMatriculaAlunoResponse = await _fixture.ChamaEndpointPagarMatriculaAluno(_fixture.UsuarioLogado.Id, pagamentoModel);
            }

            pagarMatriculaAlunoResponse.EnsureSuccessStatusCode();

            var finalizarAulaModel = new FinalizarAulaViewModel
            {
                CursoId = cursoId,
                AulaId = Guid.NewGuid()
            };


            // Act

            var finalizarAulaResponse = await _fixture.ChamaEndpointFinalizarAulaAluno(_fixture.UsuarioLogado.Id, finalizarAulaModel);


            // Assert
            finalizarAulaResponse.EnsureSuccessStatusCode();
            var finalizarAulaResponseContent = await finalizarAulaResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();
            Assert.NotNull(finalizarAulaResponseContent);
            Assert.True(gestaoContext.Matriculas.Any());
            Assert.NotNull(gestaoContext.Matriculas.First(x => x.AlunoId == _fixture.UsuarioLogado.Id && x.Curso.CursoId == matriculaViewModel.CursoId));
            Assert.True(gestaoContext.Matriculas.First(x => x.AlunoId == _fixture.UsuarioLogado.Id && x.Curso.CursoId == matriculaViewModel.CursoId).Curso.HistoricoAprendizado!.Progresso > 0);

        }


        [Fact(DisplayName = "Finalizar Aula Em Curso Inexistente")]
        [Trait("Categoria", "Integracao API - Alunos")]
        public async Task AlunoFinalizarAula_AlunoCadastradoFinalizaAulaEmCursoNaoMatriculado_DeveRetornarErro()
        {
            // Arrange
            await _fixture.CadastrarLogarAdministrador();
            var cadastrarCurso = await _fixture.ChamaEndpointCadastraCursoValido();
            cadastrarCurso.EnsureSuccessStatusCode();
            var gestaoContext = _fixture.ObterServico<GestaoContext>();
            var cursosContext = _fixture.ObterServico<CursoContext>();
            var cursoId = cursosContext.Cursos.First().Id;
            await _fixture.CadastrarLogarAluno(_cadastrarAlunoViewModelValido);

            var matriculaViewModel = new MatricularAlunoViewModel
            {
                CursoId = cursoId,
                CursoNome = "Curso Teste",
                CursoValor = 1000m,
                CursoTotalAulas = 10
            };
            var matricularAlunoResponse = await _fixture.ChamaEndpointMatricularAluno(_fixture.UsuarioLogado.Id, matriculaViewModel);
            matricularAlunoResponse.EnsureSuccessStatusCode();

            var pagamentoModel = new PagamentoMatriculaViewModel
            {
                CursoId = cursoId,
                NomeCartao = "Nome do Cartão",
                NumeroCartao = "1234567890123456",
                MesAnoExpiracao = "12/25",
                Ccv = "123"
            };


            var pagarMatriculaAlunoResponse = await _fixture.ChamaEndpointPagarMatriculaAluno(_fixture.UsuarioLogado.Id, pagamentoModel);
            while (gestaoContext.Matriculas.Any(x => x.AlunoId == _fixture.UsuarioLogado.Id && x.Curso.CursoId == matriculaViewModel.CursoId && x.Status == StatusMatriculaEnum.PENDENTE_PAGAMENTO))
            {

                pagarMatriculaAlunoResponse = await _fixture.ChamaEndpointPagarMatriculaAluno(_fixture.UsuarioLogado.Id, pagamentoModel);
            }

            pagarMatriculaAlunoResponse.EnsureSuccessStatusCode();

            var finalizarAulaModel = new FinalizarAulaViewModel
            {
                CursoId = Guid.NewGuid(),
                AulaId = Guid.NewGuid()
            };


            // Act

            var finalizarAulaResponse = await _fixture.ChamaEndpointFinalizarAulaAluno(_fixture.UsuarioLogado.Id, finalizarAulaModel);


            // Assert            
            var finalizarAulaResponseContent = await finalizarAulaResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();            
            Assert.NotNull(finalizarAulaResponseContent);
            Assert.True(gestaoContext.Matriculas.Any());
            Assert.True(!finalizarAulaResponseContent.Success && finalizarAulaResponseContent.Errors!.Contains(Aluno.AlunoNaoEstaMatriculado));

        }

        [Fact(DisplayName = "Obter Matriculas Aluno Cadastrado e Matriculado")]
        [Trait("Categoria", "Integracao API - Alunos")]
        public async Task ObterMatriculasAluno_AlunoCadastradoEMatruculado_DeveRetornarAlunoComListaDeMatriculas()
        {
            // Arrange
            await _fixture.CadastrarLogarAdministrador();
            var cadastrarCurso = await _fixture.ChamaEndpointCadastraCursoValido();
            cadastrarCurso.EnsureSuccessStatusCode();
            var gestaoContext = _fixture.ObterServico<GestaoContext>();
            var cursosContext = _fixture.ObterServico<CursoContext>();
            var cursoId = cursosContext.Cursos.First().Id;
            await _fixture.CadastrarLogarAluno(_cadastrarAlunoViewModelValido);

            var matriculaViewModel = new MatricularAlunoViewModel
            {
                CursoId = cursoId,
                CursoNome = "Curso Teste",
                CursoValor = 1000m,
                CursoTotalAulas = 10
            };
            var matricularAlunoResponse = await _fixture.ChamaEndpointMatricularAluno(_fixture.UsuarioLogado.Id, matriculaViewModel);
            matricularAlunoResponse.EnsureSuccessStatusCode();

            // Act
            var alunoQueriesResponse = await _fixture.ChamaEndpointObterMatriculasAluno(_fixture.UsuarioLogado.Id);
            // Assert

            alunoQueriesResponse.EnsureSuccessStatusCode();
            var a = await alunoQueriesResponse.Content.ReadAsStringAsync();
            var alunoQueriesResponseContent = await alunoQueriesResponse.Content.ReadFromJsonAsync<MatriculasAlunoViewModel>();
            Assert.NotNull(alunoQueriesResponseContent);            
            Assert.Equal(alunoQueriesResponseContent.Nome, _cadastrarAlunoViewModelValido.Nome);
            Assert.NotEmpty(alunoQueriesResponseContent.Matriculas);            
        }



        private CadastrarAlunoViewModel ObterCadastrarAlunoViewModelValido()
        {
            var alunoCommandValido = _alunoTestsFixture.GerarCadastrarAlunoCommandInvalido(_alunoValido);
            return new CadastrarAlunoViewModel
            {
                Nome = alunoCommandValido.Nome,
                Email = alunoCommandValido.Email,
                Senha = alunoCommandValido.Senha,
                ConfirmarSenha = alunoCommandValido.Senha
            };
        }        
    }
}
