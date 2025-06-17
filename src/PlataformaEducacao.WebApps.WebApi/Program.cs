using PlataformaEducacao.WebApps.WebApi.Configurations;
using PlataformaEducacao.WebApps.WebApi.Extensions;
using PlataformaEducacao.WebApps.WebApi.Extensions.Migrations;


var builder = WebApplication.CreateBuilder(args);

builder
    .AddApiConfiguration()
    .AddIdentityConfiguration()
    .AddMapperConfiguration()
    .AddContextsConfiguration()
    .AddSwaggerConfiguration()    
    .ResolveDependencies();

var app = builder.Build();


app.AddSwagger();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.ApplyMigrations();

app.Run();
