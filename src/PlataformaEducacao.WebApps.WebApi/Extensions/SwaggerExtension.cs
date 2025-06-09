namespace PlataformaEducacao.WebApps.WebApi.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
