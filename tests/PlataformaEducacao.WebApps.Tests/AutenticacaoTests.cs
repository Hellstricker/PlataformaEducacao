using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PlataformaEducacao.WebApps.Tests.Configs;
using PlataformaEducacao.WebApps.WebApi.ViewModels;
using System.Net.Http.Json;

namespace PlataformaEducacao.WebApps.Tests
{
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class AutenticacaoTests
    {
        private readonly IntegrationTestsFisxture<Program> _testsFixture;
        private readonly CadastrarUsuarioViewModel cadastrarUsuarioViewModelValido;
        

        public AutenticacaoTests(IntegrationTestsFisxture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
            cadastrarUsuarioViewModelValido = new CadastrarUsuarioViewModel
            {
                Nome = "Joao da Silva Um",
                Email = "joaoteste1@gmail.com",
                Senha = "SenhaForte123!",
                ConfirmarSenha = "SenhaForte123!",
            };
        }

        [Fact(DisplayName = "Realizar cadastro de admministrador com sucesso")]
        [Trait("Categoria", "Integracao API - Autenticacao")]
        public async Task CadastrarAdministrador_NovoAdministrador_DeveRetornarComSucesso()
        {
            //Arrange            
            var userManager = _testsFixture.ObterServico<UserManager<IdentityUser<Guid>>>();


            //Act
            var postResponse =  await _testsFixture.ChamaEndpointCadastraUsuarioValido(cadastrarUsuarioViewModelValido);


            // Assert
            postResponse.EnsureSuccessStatusCode();
            var responseContent = await postResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();
            Assert.True(responseContent!.Success);
            var user = await userManager.FindByEmailAsync(cadastrarUsuarioViewModelValido.Email!);
            Assert.NotNull(user);
            Assert.Equal(cadastrarUsuarioViewModelValido.Email, user.Email);
            Assert.Equal(cadastrarUsuarioViewModelValido.Nome, user.UserName);
        }

        [Fact(DisplayName = "Realizar login com sucesso")]
        [Trait("Categoria", "Integracao API - Autenticacao")]
        public async Task RealizarLogin_UsuarioQualquerPerfil_DeveRetornarComSucesso()
        {
            // Arrange
            var postResponseArrange = await _testsFixture.ChamaEndpointCadastraUsuarioValidoLogin();
            postResponseArrange.EnsureSuccessStatusCode();

            // Act
            var postResponse = await _testsFixture.ChamaEndpointLogarUsuarioValido();
            // Assert
            postResponse.EnsureSuccessStatusCode();

            var result = await postResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();
            var userData = _testsFixture.DeserializeFromObject<LoginResponseViewModel>(result!.Data!);
            Assert.NotEmpty(userData.AccessToken!);
        }


    }

}