using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlataformaEducacao.Cadastros.Data;
using PlataformaEducacao.Gestao.Data;
using PlataformaEducacao.Pagamentos.Data;
using PlataformaEducacao.WebApps.WebApi.Contexts;

namespace PlataformaEducacao.WebApps.Tests.Configs
{
    public class PlataformaEducacaoAppFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment("Testes");

            builder.ConfigureServices((context, services) =>
            {
                // Remove o DbContext original
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<CursoContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<GestaoContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<PagamentosContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                

                var connectionString = context.Configuration.GetConnectionString("DefaultConnection");

                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase(connectionString!);
                });

                services.AddDbContext<CursoContext>(options =>
                {
                    options.UseInMemoryDatabase(connectionString!);
                });

                services.AddDbContext<GestaoContext>(options =>
                {
                    options.UseInMemoryDatabase(connectionString!);
                });

                services.AddDbContext<PagamentosContext>(options =>
                {
                    options.UseInMemoryDatabase(connectionString!);
                });

            });


            var host = base.CreateHost(builder);

            using (var scope = host.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                var db2 = scopedServices.GetRequiredService<GestaoContext>();
                var db3 = scopedServices.GetRequiredService<CursoContext>();
                var db4 = scopedServices.GetRequiredService<PagamentosContext>();
                var logger = scopedServices.GetRequiredService<ILogger<PlataformaEducacaoAppFactory<TProgram>>>();

                try
                {
                    db.Database.EnsureCreated();
                    db2.Database.EnsureCreated();
                    db3.Database.EnsureCreated();
                    db4.Database.EnsureCreated();
                    logger.LogInformation("Banco de dados de teste criado com sucesso.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Erro ao criar o banco de dados de teste.");
                    throw;
                }
            }

            return host;
        }
    }
}
