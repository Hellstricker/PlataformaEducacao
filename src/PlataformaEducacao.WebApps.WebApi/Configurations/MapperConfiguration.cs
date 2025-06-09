namespace PlataformaEducacao.WebApps.WebApi.Configurations
{
    public static class MapperConfiguration
    {
        public static WebApplicationBuilder AddMapperConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(
                typeof(GestaoAlunos.Application.Mappings.DtoToDomainMappingProfile),
                typeof(GestaoCursos.Application.Mappings.DtoToDomainMappingProfile),
                typeof(GestaoCursos.Application.Mappings.DomainToDtoMappingProfile),
                typeof(GestaoAlunos.Application.Mappings.DomainToDtoMappingProfile)
                );
            return builder;
        }
    }
}
