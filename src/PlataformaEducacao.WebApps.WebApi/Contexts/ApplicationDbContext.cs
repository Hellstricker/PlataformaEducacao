using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PlataformaEducacao.WebApps.WebApi.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

    }
}
