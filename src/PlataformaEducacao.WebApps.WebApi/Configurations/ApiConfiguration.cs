using PlataformaEducacao.WebApps.WebApi.Filters;

namespace PlataformaEducacao.WebApps.WebApi.Configurations
{
    public static class ApiConfiguration
    {
        public static WebApplicationBuilder AddApiConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers((options =>
            {
                options.Filters.Add(new GlobalExceptionFilter());
            }));
            return builder;
        }
    }
}

