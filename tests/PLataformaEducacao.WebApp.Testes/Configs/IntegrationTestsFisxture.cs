using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;

namespace PlataformaEducacao.WebApp.Tests.Configs
{

    [CollectionDefinition(nameof(IntegrationApiTestsFixtureCollection))]
    public class IntegrationApiTestsFixtureCollection : ICollectionFixture<IntegrationTestsFisxture<Program>>{}
    
    //[CollectionDefinition(nameof(IntegrationApiGestaoTestsFixtureCollection))]
    //public class IntegrationApiGestaoTestsFixtureCollection : 
    //    ICollectionFixture<IntegrationTestsFisxture<Program>>,
    //    ICollectionFixture<CadastrarAlunoCommandTestsFixture>
    //{}


    public class IntegrationTestsFisxture<TProgram> : IDisposable where TProgram : class
    {
        public readonly PlataformaEducacaoAppFactory<TProgram> Factory;
        public HttpClient Client;
        public string BearerToken => string.Empty;

        public IntegrationTestsFisxture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions
            {            
            };            

            Factory = new PlataformaEducacaoAppFactory<TProgram>();
            Client = Factory.CreateClient(clientOptions);
            
        }
        public void Dispose()
        {
            Client.Dispose();
            Factory.Dispose();
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
    }

    
}
