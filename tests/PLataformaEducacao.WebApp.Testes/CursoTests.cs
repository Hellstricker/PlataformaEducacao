//using Microsoft.Extensions.DependencyInjection;
//using PlataformaEducacao.Cadastros.Application.ViewModels;
//using PlataformaEducacao.Cadastros.Data;
//using PlataformaEducacao.WebApp.Tests.Configs;
//using PlataformaEducacao.WebApps.WebApi.ViewModels;
//using System.Net.Http.Json;

//namespace PLataformaEducacao.WebApp.Testes
//{
//    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
//    public class CursoTests
//    {
//        private readonly IntegrationTestsFisxture<Program> _testsFixture;

//        private readonly CadastrarUsuarioViewModel cadastrarUsuarioViewModelValidoLogin;
//        private readonly LoginViewModel loginViewModelValido;
//        private readonly CursoViewModel cursoValidoViewModel;
//        private readonly AulaViewModel aulaValidaViewModel;
//        private string _token = string.Empty;


//        public CursoTests(IntegrationTestsFisxture<Program> testsFixture)
//        {
//            _testsFixture = testsFixture;
//            cadastrarUsuarioViewModelValidoLogin = new CadastrarUsuarioViewModel
//            {
//                Nome = "Joao da Silva Tres",
//                Email = "joaoteste3@gmail.com",
//                Senha = "SenhaForte123!",
//                ConfirmarSenha = "SenhaForte123!",
//            };

//            loginViewModelValido = new LoginViewModel
//            {
//                Email = cadastrarUsuarioViewModelValidoLogin.Email,
//                Senha = cadastrarUsuarioViewModelValidoLogin.Senha
//            };

//            cursoValidoViewModel = new CursoViewModel
//            {
//                Titulo = "Curso de Testes Automatizados",
//                Descricao = "Curso completo sobre testes automatizados com .NET",
//                CargaHoraria = 40,
//                Valor = 100.01M
//            };

//            aulaValidaViewModel = new AulaViewModel
//            {
//                Titulo = "Titulo aula",
//                Duracao = 10,
//                Conteudo = "Conteudo Aula 1"
//            };
//        }
//        [Fact(DisplayName = "Realizar cadastro de curso com sucesso")]
//        [Trait("Categoria", "Integracao API - Cursos")]
//        public async Task CadastrarCurso_AdministradorCadastraCurso_DeveRetornarComSucesso()
//        {
//            //Arrange
//            await LogarUsuario();
//            using var scope = _testsFixture.Factory.Services.CreateScope();
//            var cursoDbContext = scope.ServiceProvider.GetRequiredService<CursoContext>();

//            //Act
//            var postCadastrarCursoResponse = await _testsFixture.Client.PostAsJsonAsync("api/Cursos", cursoValidoViewModel);

//            //Assert
//            postCadastrarCursoResponse.EnsureSuccessStatusCode();
//            var postCadastrarCursoResponseContent = await postCadastrarCursoResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();
//            Assert.NotNull(postCadastrarCursoResponseContent);
//            Assert.True(postCadastrarCursoResponseContent!.Success);
//            Assert.True(cursoDbContext.Cursos.Any());
//        }

//        [Fact(DisplayName = "Realizar cadastro de aula com sucesso")]
//        [Trait("Categoria", "Integracao API - Cursos")]
//        public async Task CadastrarAula_AdministradorCadastraAula_DeveRetornarComSucesso()
//        {
//            //Arrange
//            await LogarUsuario();
//            using var scope = _testsFixture.Factory.Services.CreateScope();
//            var cursoDbContext = scope.ServiceProvider.GetRequiredService<CursoContext>();
//            if (!cursoDbContext.Cursos.Any())
//                await CadastrarCurso_AdministradorCadastraCurso_DeveRetornarComSucesso();
//            var cursoId = cursoDbContext.Cursos.First().Id;
//            aulaValidaViewModel.CursoId = cursoId;

//            //Act
//            var postCadastrarCursoResponse = await _testsFixture.Client.PostAsJsonAsync($"api/cursos/{cursoId}/cadastrar-aula", aulaValidaViewModel);

//            //Assert
//            var a = await postCadastrarCursoResponse.Content.ReadAsStringAsync();
//            postCadastrarCursoResponse.EnsureSuccessStatusCode();
//            var postCadastrarCursoResponseContent = await postCadastrarCursoResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();
//            Assert.NotNull(postCadastrarCursoResponseContent);
//            Assert.True(postCadastrarCursoResponseContent!.Success);
//            Assert.True(cursoDbContext.Aulas.Any());
//        }

//        private async Task LogarUsuario()
//        {
//            if (_testsFixture.Client.DefaultRequestHeaders.Authorization == null)
//            {
//                var postCadastrarUsuarioArrange = await _testsFixture.Client.PostAsJsonAsync("api/autenticacao/cadastrar-administrador", cadastrarUsuarioViewModelValidoLogin);
//                postCadastrarUsuarioArrange.EnsureSuccessStatusCode();
//                var postLogarUsuarioArrange = await _testsFixture.Client.PostAsJsonAsync("api/autenticacao/login", loginViewModelValido);
//                postLogarUsuarioArrange.EnsureSuccessStatusCode();
//                var postLogaUsuarioArrangeContent = await postLogarUsuarioArrange.Content.ReadFromJsonAsync<BaseResultViewModel>();
//                var userData = _testsFixture.DeserializeFromObject<LoginResponseViewModel>(postLogaUsuarioArrangeContent!.Data!);
//                _token = userData!.AccessToken!;
//                _testsFixture.Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
//            }
//        }


//        //[Fact(DisplayName = "Realizar cadastro de curso com sucesso")]
//        //[Trait("Categoria", "Integracao API - Cursos")]
//        //public async Task CadastrarCurso_AdministradorCadastraCurso_DeveRetornarComSucesso()
//        //{
//        //    //Arrange
//        //    await LogarUsuario();
//        //    using var scope = _testsFixture.Factory.Services.CreateScope();
//        //    var cursoDbContext = scope.ServiceProvider.GetRequiredService<CursoContext>();

//        //    //Act
//        //    var postCadastrarCursoResponse = await _testsFixture.Client.PostAsJsonAsync("api/Cursos", cursoValidoViewModel);

//        //    //Assert
//        //    postCadastrarCursoResponse.EnsureSuccessStatusCode();
//        //    var postCadastrarCursoResponseContent = await postCadastrarCursoResponse.Content.ReadFromJsonAsync<BaseResultViewModel>();
//        //    Assert.NotNull(postCadastrarCursoResponseContent);
//        //    Assert.True(postCadastrarCursoResponseContent!.Success);
//        //    Assert.True(cursoDbContext.Cursos.Any());
//        //}

//        //private async Task LogarUsuario()
//        //{
//        //    if (_testsFixture.Client.DefaultRequestHeaders.Authorization == null)
//        //    {
//        //        var postCadastrarUsuarioArrange = await _testsFixture.Client.PostAsJsonAsync("api/autenticacao/cadastrar-administrador", cadastrarUsuarioViewModelValidoLogin);
//        //        postCadastrarUsuarioArrange.EnsureSuccessStatusCode();
//        //        var postLogarUsuarioArrange = await _testsFixture.Client.PostAsJsonAsync("api/autenticacao/login", loginViewModelValido);
//        //        postLogarUsuarioArrange.EnsureSuccessStatusCode();
//        //        var postLogaUsuarioArrangeContent = await postLogarUsuarioArrange.Content.ReadFromJsonAsync<BaseResultViewModel>();
//        //        var userData = _testsFixture.DeserializeFromObject<LoginResponseViewModel>(postLogaUsuarioArrangeContent!.Data!);
//        //        _token = userData!.AccessToken!;
//        //       _testsFixture.Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
//        //    }
//        //}
//    }
//}