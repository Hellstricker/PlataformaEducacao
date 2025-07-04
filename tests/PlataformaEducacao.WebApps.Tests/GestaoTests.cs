using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PlataformaEducacao.Cadastros.Data;
using PlataformaEducacao.Gestao.Application.Tests.Configs;
using PlataformaEducacao.Gestao.Data;
using PlataformaEducacao.Gestao.Domain;
using PlataformaEducacao.Gestao.Domain.Tests.Configs;
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
        private readonly GestaoDomainTestsFixture _gestaoDomainTestsFixture;


        private readonly Aluno _alunoValido;
        private readonly CadastrarAlunoViewModel _cadastrarAlunoViewModelValido;

        public GestaoTests(IntegrationTestsFisxture<Program> fisxture, GestaoApplicationTestsFixture alunoTestsFixture, GestaoDomainTestsFixture gestaoDomainTestsFixture)
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
