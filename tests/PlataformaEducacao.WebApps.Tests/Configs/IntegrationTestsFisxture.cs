using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using PlataformaEducacao.Cadastros.Application.ViewModels;
using PlataformaEducacao.Cadastros.Data;
using PlataformaEducacao.Cadastros.Domain;
using PlataformaEducacao.Gestao.Application.Commands;
using PlataformaEducacao.Gestao.Application.Tests.Configs;
using PlataformaEducacao.Gestao.Domain.Tests.Configs;
using PlataformaEducacao.WebApps.WebApi.Contexts;
using PlataformaEducacao.WebApps.WebApi.ViewModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace PlataformaEducacao.WebApps.Tests.Configs
{

    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection :
        ICollectionFixture<IntegrationTestsFisxture<Program>>,
        ICollectionFixture<GestaoApplicationTestsFixture>,
        ICollectionFixture<AlunoTestsFixture>

    { }

    public class IntegrationTestsFisxture<TProgram> : IDisposable where TProgram : class
    {
        public readonly PlataformaEducacaoAppFactory<TProgram> Factory;
        public HttpClient Client;
        public string BearerToken => string.Empty;

        private IServiceScope _scope;


        public IdentityUser<Guid> UsuarioLogado { get; private set; } = null!;
        public Curso CursoCadastrado { get; private set; } = null!;
        
        private string _token = string.Empty;

        //ViewModels
        private readonly CursoViewModel cursoValidoViewModel;
        private readonly CadastrarUsuarioViewModel cadastrarUsuarioViewModelValidoLogin;
        private readonly LoginViewModel loginViewModelValido;
        private readonly AulaViewModel aulaValidaViewModel;        


        public IntegrationTestsFisxture()
        {

            var clientOptions = new WebApplicationFactoryClientOptions
            {            
            };            

            Factory = new PlataformaEducacaoAppFactory<TProgram>();
            Client = Factory.CreateClient(clientOptions);
            _scope = Factory.Services.CreateScope();


            cadastrarUsuarioViewModelValidoLogin = new CadastrarUsuarioViewModel
            {
                Nome = "Joao da Silva Tres",
                Email = "joaoteste3@gmail.com",
                Senha = "SenhaForte123!",
                ConfirmarSenha = "SenhaForte123!",
            };
            
            loginViewModelValido = new LoginViewModel
            {
                Email = cadastrarUsuarioViewModelValidoLogin.Email,
                Senha = cadastrarUsuarioViewModelValidoLogin.Senha
            };

            cursoValidoViewModel = new CursoViewModel
            {
                Titulo = "Curso de Testes Automatizados",
                Descricao = "Curso completo sobre testes automatizados com .NET",
                CargaHoraria = 40,
                Valor = 100.01M
            };

            aulaValidaViewModel = new AulaViewModel
            {
                Titulo = "Titulo aula",
                Duracao = 10,
                Conteudo = "Conteudo Aula 1"
            };
            
        }

        public T ObterServico<T>() where T : class
        {
            return _scope.ServiceProvider.GetRequiredService<T>();
        }

        public async Task<HttpResponseMessage> ChamaEndpointCadastraUsuarioValido(CadastrarUsuarioViewModel model)
        {
            return await Client.PostAsJsonAsync("api/autenticacao/cadastrar-administrador", model);
        }

        public async Task<HttpResponseMessage> ChamaEndpointCadastraUsuarioValidoLogin()
        {
            await RemoverUsuario(cadastrarUsuarioViewModelValidoLogin.Email!);
            return await ChamaEndpointCadastraUsuarioValido(cadastrarUsuarioViewModelValidoLogin);
        }

        public async Task<HttpResponseMessage> ChamaEndpointLogarUsuarioValido()
        {
            return await Client.PostAsJsonAsync("api/autenticacao/login", loginViewModelValido);
        }

        public async Task<HttpResponseMessage> ChamaEndpointCadastraCursoValido()
        {
            return await Client.PostAsJsonAsync("api/Cursos", cursoValidoViewModel);
        }

        public async Task<HttpResponseMessage> ChamaEndpointCadastrarAulaValida(Guid cursoId)
        {
            aulaValidaViewModel.CursoId = cursoId;
            return await ChamaEndpointCadastrarAula(cursoId, aulaValidaViewModel);
        }

        public async Task<HttpResponseMessage> ChamaEndpointCadastrarAluno(CadastrarAlunoViewModel cadastrarAlunoViewModel)
        {
            return await Client.PostAsJsonAsync("api/alunos/", cadastrarAlunoViewModel);
        }

        public async Task<HttpResponseMessage> ChamaEndpointMatricularAluno(Guid alunoId, MatricularAlunoViewModel matricularAlunoViewModel)
        {
            return await Client.PostAsJsonAsync($"api/alunos/{alunoId}/matricular", matricularAlunoViewModel);
        }

        public async Task<HttpResponseMessage> ChamaEndpointPagarMatriculaAluno(Guid alunoId, PagamentoMatriculaViewModel pagamentoMatriculaAlunoViewModel)
        {
            return await Client.PostAsJsonAsync($"api/alunos/{alunoId}/pagar-matricula-curso", pagamentoMatriculaAlunoViewModel);
        }

        public async Task<HttpResponseMessage> ChamaEndpointFinalizarAulaAluno(Guid alunoId, FinalizarAulaViewModel finalizarAulaViewModel)
        {
            return await Client.PostAsJsonAsync($"api/alunos/{alunoId}/finalizar-aula", finalizarAulaViewModel);
        }

        public async Task<HttpResponseMessage> ChamaEndpointObterMatriculasAluno(Guid alunoId)
        {
            return await Client.GetAsync($"api/alunos/{alunoId}/minhas-matriculas");
        }
        


        private async Task<HttpResponseMessage> ChamaEndpointCadastrarAula(Guid cursoId, AulaViewModel aulaViewModel)
        {            
            return await Client.PostAsJsonAsync($"api/cursos/{cursoId}/cadastrar-aula", aulaViewModel);
        }

        public Guid ObtemIdCursoCadastrado()
        {
            var cursoDbContext = ObterServico<CursoContext>();
            if (!cursoDbContext.Cursos.Any()) return Guid.NewGuid();
            return cursoDbContext.Cursos.First().Id;            
        }

        private CadastrarAlunoViewModel ObterCadastrarAlunoViewModelValido(CadastrarAlunoCommand alunoCommandValido)
        {            
            return new CadastrarAlunoViewModel
            {
                Nome = alunoCommandValido.Nome,
                Email = alunoCommandValido.Email,
                Senha = alunoCommandValido.Senha,
                ConfirmarSenha = alunoCommandValido.Senha
            };
        }

        private async Task SetUsuarioLogado(string email)
        {
            var userManager = ObterServico<UserManager<IdentityUser<Guid>>>();
            UsuarioLogado = await userManager.FindByEmailAsync(email);
        }


        public async Task CadastrarLogarAdministrador()
        {
            Client.DefaultRequestHeaders.Authorization = null;
                            
                var postCadastrarUsuarioArrange = await ChamaEndpointCadastraUsuarioValidoLogin();
                postCadastrarUsuarioArrange.EnsureSuccessStatusCode();
                loginViewModelValido.Senha = cadastrarUsuarioViewModelValidoLogin.Senha;
                loginViewModelValido.Email = cadastrarUsuarioViewModelValidoLogin.Email;
                var postLogarUsuarioArrange = await ChamaEndpointLogarUsuarioValido();                                
                _token = await GetAuthorizationTokenFromLoginResponse(postLogarUsuarioArrange);
                await SetUsuarioLogado(cadastrarUsuarioViewModelValidoLogin.Email!);
                SetAuthorizationHeader(_token);
                        
        }

        private async Task RemoverUsuario(string email)
        {
            var context = ObterServico<ApplicationDbContext>();
            var usuario = context.Users.FirstOrDefault(u => u.Email == email);
            if(usuario == null) return;
            context.Users.Remove(usuario); 
            await context.SaveChangesAsync();
        }

        public async Task CadastrarLogarAluno(CadastrarAlunoViewModel cadastrarAlunoViewModel)
        {
            Client.DefaultRequestHeaders.Authorization = null;
            
                var postCadastrarUsuarioArrange = await ChamaEndpointCadastrarAluno(cadastrarAlunoViewModel);
                postCadastrarUsuarioArrange.EnsureSuccessStatusCode();
                loginViewModelValido.Senha = cadastrarAlunoViewModel.Senha;
                loginViewModelValido.Email = cadastrarAlunoViewModel.Email; 
                var postLogarUsuarioArrange = await ChamaEndpointLogarUsuarioValido();
                await SetUsuarioLogado(cadastrarAlunoViewModel.Email);
                _token = await GetAuthorizationTokenFromLoginResponse(postLogarUsuarioArrange);
                SetAuthorizationHeader(_token);
            
        }

        private async Task<string> GetAuthorizationTokenFromLoginResponse(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            if (response == null) return string.Empty;
            var responseContent = await response.Content.ReadFromJsonAsync<BaseResultViewModel>();
            var userData = DeserializeFromObject<LoginResponseViewModel>(responseContent!.Data!);
            _token = userData!.AccessToken!;
            return _token;
        }

        private void SetAuthorizationHeader(string token)
        {
            if (Client.DefaultRequestHeaders.Authorization != null)
            {
                Client.DefaultRequestHeaders.Authorization = null;
            }
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }


        public T DeserializeFromObject<T>(object data)
        {
            var content = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            })!;
        }

        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
        }

        
    }

    
}
