using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.GestaoAlunos.Data;
using PlataformaEducacao.GestaoCursos.Data;
using PlataformaEducacao.Pagamentos.Data;
using PlataformaEducacao.WebApps.WebApi.Contexts;

namespace PlataformaEducacao.WebApps.WebApi.Configurations
{

    public static class ContextsConfiguration
    {
        public static WebApplicationBuilder AddContextsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<GestaoCursosContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddDbContext<GestaoAlunosContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddDbContext<PagamentosContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            return builder;
        }
    }
}
