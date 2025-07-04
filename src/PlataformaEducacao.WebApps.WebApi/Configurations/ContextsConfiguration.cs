using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Cadastros.Data;
using PlataformaEducacao.Gestao.Data;
using PlataformaEducacao.WebApps.WebApi.Contexts;

namespace PlataformaEducacao.WebApps.WebApi.Configurations
{
    public static class ContextsConfiguration
    {
        public static WebApplicationBuilder AddContextsConfiguration(this WebApplicationBuilder builder)
        {            
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddDbContext<CursoContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            builder.Services.AddDbContext<GestaoContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            return builder;
        }
    }
}
