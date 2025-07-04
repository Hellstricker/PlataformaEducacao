using Microsoft.Extensions.DependencyInjection;
using PlataformaEducacao.Cadastros.Application.ViewModels;
using PlataformaEducacao.Cadastros.Data;
using PlataformaEducacao.WebApps.Tests.Configs;
using PlataformaEducacao.WebApps.WebApi.ViewModels;
using System.Net.Http.Json;

namespace PLataformaEducacao.WebApps.Testes
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class CursoTests
    {
        private readonly IntegrationTestsFisxture<Program> _testsFixture;
              
                
        private string _token = string.Empty;


        public CursoTests(IntegrationTestsFisxture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
            _testsFixture.Client.DefaultRequestHeaders.Authorization = null;
        }
        [Fact(DisplayName = "Realizar cadastro de curso com sucesso")]
        [Trait("Categoria", "Integracao API - Cursos")]
        public async Task CadastrarCurso_AdministradorCadastraCurso_DeveRetornarComSucesso()
        {
            //Arrange
            await _testsFixture.CadastrarLogarAdministrador();            
            var cursoDbContext = _testsFixture.ObterServico<CursoContext>();

            //Act
            var postCadastrarCursoResponse = await _testsFixture.ChamaEndpointCadastraCursoValido();

            //Assert
            postCadastrarCursoResponse.EnsureSuccessStatusCode();
            var postCadastrarCursoResponseContent = await postCadastrarCursoResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();
            Assert.NotNull(postCadastrarCursoResponseContent);
            Assert.True(postCadastrarCursoResponseContent!.Success);
            Assert.True(cursoDbContext.Cursos.Any());
        }

        [Fact(DisplayName = "Realizar cadastro de aula com sucesso")]
        [Trait("Categoria", "Integracao API - Cursos")]
        public async Task CadastrarAula_AdministradorCadastraAula_DeveRetornarComSucesso()
        {
            //Arrange            
            await _testsFixture.CadastrarLogarAdministrador();                        
            var cadastrarCursoResponse = await _testsFixture.ChamaEndpointCadastraCursoValido();
            cadastrarCursoResponse.EnsureSuccessStatusCode();

            var cursoDbContext = _testsFixture.ObterServico<CursoContext>();
            var cursoId = _testsFixture.ObtemIdCursoCadastrado();            

            //Act
            var cadastrarAulaResponse = await _testsFixture.ChamaEndpointCadastrarAulaValida(cursoId);

            //Assert            
            cadastrarAulaResponse.EnsureSuccessStatusCode();
            var cadastrarAulaResponseContent = await cadastrarAulaResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();
            Assert.NotNull(cadastrarAulaResponseContent);
            Assert.True(cadastrarAulaResponseContent!.Success);
            Assert.True(cursoDbContext.Aulas.Any());
        }

        
    }
}