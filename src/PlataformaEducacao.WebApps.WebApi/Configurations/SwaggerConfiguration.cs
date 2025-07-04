using Microsoft.OpenApi.Models;

namespace PlataformaEducacao.WebApps.WebApi.Configurations
{
    public static class SwaggerConfiguration
    {
        public static WebApplicationBuilder AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT neste formato : Bearer {TOKEN}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                        Array.Empty<string>()
                    }
                });
            });
            return builder;
        }
    }
}
