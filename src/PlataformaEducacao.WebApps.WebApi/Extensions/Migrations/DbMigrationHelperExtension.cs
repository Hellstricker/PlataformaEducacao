using PlataformaEducacao.WebApps.WebApi.Helpers;

namespace PlataformaEducacao.WebApps.WebApi.Extensions.Migrations
{
    public static class DbMigrationHelperExtension
    {
        public static void ApplyMigrations(this WebApplication app)
        {
            DbMigrationHelpers.EnsureSeedData(app).GetAwaiter().GetResult();
        }
    }
}
