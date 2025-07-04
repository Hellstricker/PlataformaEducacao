using PlataformaEducacao.WebApps.WebApi.Configurations;
using PlataformaEducacao.WebApps.WebApi.Extensions.Migrations;
using PlataformaEducacao.WebApps.WebApi.Extensions.Swaggers;

public partial class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder
            .ResolveDependencies()
            .AddContextsConfiguration()
            .AddIdentityConfiguration()
            .AddSwaggerConfiguration();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.AddSwagger();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.ApplyMigrations();

        app.Run();
    }
}