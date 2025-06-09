using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.GestaoAlunos.Data;
using PlataformaEducacao.GestaoAlunos.Domain;
using PlataformaEducacao.GestaoCursos.Data;
using PlataformaEducacao.GestaoCursos.Domain;
using PlataformaEducacao.Pagamentos.Data;
using PlataformaEducacao.WebApps.WebApi.Contexts;
using PlataformaEducacao.WebApps.WebApi.Enums;

namespace PlataformaEducacao.WebApps.WebApi.Helpers
{
    public static class DbMigrationHelpers
    {
        public static async Task EnsureSeedData(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedData(services);
        }

        private static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {            
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var environment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
            
            var userContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var cursosContext = scope.ServiceProvider.GetRequiredService<GestaoCursosContext>();
            var alunosContext = scope.ServiceProvider.GetRequiredService<GestaoAlunosContext>();
            var pagamentosContext = scope.ServiceProvider.GetRequiredService<PagamentosContext>();

            if(environment.IsDevelopment())
            {
                await userContext.Database.MigrateAsync();
                await cursosContext.Database.MigrateAsync();
                await alunosContext.Database.MigrateAsync();
                await pagamentosContext.Database.MigrateAsync();

                await EnsureSeed(userContext, alunosContext, cursosContext);
            }
        }

        private static async Task EnsureSeed(ApplicationDbContext userContext, GestaoAlunosContext alunosContext, GestaoCursosContext cursosContext)
        {
            if (cursosContext.Cursos.Any())
                return;

            var curso = new Curso("Como criar uma plataforma de ensino", 1500.35M, new ConteudoProgramatico("Ensinar pessoas a criar uma plataforma de ensino", "C#, EntityFrameworkCore, DDD, TDD, WEB API, ASP.NET Core"));
            var cursoId = curso.Id;
            cursosContext.Cursos.Add(curso);
            await cursosContext.SaveChangesAsync();

            if (cursosContext.Aulas.Any())
                return;

            var aula1 = new Aula("Introdução ao DDD", "Nesta aula vamos aprender o que é DDD e como aplicá-lo em nossos projetos.", 55, cursoId);
            var aula2 = new Aula("Introdução ao TDD", "Nesta aula vamos aprender o que é TDD e como aplicá-lo em nossos projetos.", 45, cursoId);
            var aula3 = new Aula("Introdução ao ASP.NET Core", "Nesta aula vamos aprender o que é ASP.NET Core e como aplicá-lo em nossos projetos.", 60, cursoId);
            var aula4 = new Aula("Introdução ao Entity Framework Core", "Nesta aula vamos aprender o que é Entity Framework Core e como aplicá-lo em nossos projetos.", 50, cursoId);
            var aula5 = new Aula("Introdução ao ASP.NET Core Web API", "Nesta aula vamos aprender o que é ASP.NET Core Web API e como aplicá-lo em nossos projetos.", 40, cursoId);
            var aula6 = new Aula("Introdução ao C#", "Nesta aula vamos aprender o que é C# e como aplicá-lo em nossos projetos.", 30, cursoId);

            cursosContext.Aulas.AddRange(aula1, aula2, aula3, aula4, aula5, aula6);
            await cursosContext.SaveChangesAsync();

            if(userContext.UserRoles.Any())
                return;

            IdentityRole<Guid> roleAdmin = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = nameof(PerfilUsuario.ADMIN),
                NormalizedName = nameof(PerfilUsuario.ADMIN)
            };

            IdentityRole<Guid> roleAluno = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = nameof(PerfilUsuario.ALUNO),
                NormalizedName = nameof(PerfilUsuario.ALUNO)
            };
            userContext.Roles.AddRange(roleAdmin, roleAluno);
            await cursosContext.SaveChangesAsync();

            if (userContext.Users.Any())
                return;            
            var userAdminId = Guid.NewGuid();

            IdentityUser<Guid> usuarioAdministrador = new IdentityUser<Guid>
            {
                Id = userAdminId,
                UserName = "Administrador",
                NormalizedUserName = "Administrador".ToUpper(),
                Email = "administrador@teste.com",
                NormalizedEmail = "administrador@teste.com".ToUpper(),
                AccessFailedCount = 0,
                LockoutEnabled = false,                
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            usuarioAdministrador.PasswordHash = new PasswordHasher<IdentityUser<Guid>>().HashPassword(usuarioAdministrador, "12345678");

            await userContext.Users.AddAsync(usuarioAdministrador);

            var usuarioAlunoId = Guid.NewGuid();

            IdentityUser<Guid> usuarioAluno = new IdentityUser<Guid>
            {
                Id = usuarioAlunoId,
                UserName = "Aluno Inteligente",   
                NormalizedUserName = "Aluno Inteligente".ToUpper(),
                Email = "alunointeligente@teste.com",
                NormalizedEmail = "alunointeligente@teste.com".ToUpper(),
                AccessFailedCount = 0,
                LockoutEnabled = false,                
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            usuarioAluno.PasswordHash = new PasswordHasher<IdentityUser<Guid>>().HashPassword(usuarioAluno, "12345678");

            await userContext.Users.AddAsync(usuarioAluno);
            await userContext.SaveChangesAsync();

            if(userContext.UserRoles.Any())
                return;

            userContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = userAdminId, RoleId = roleAdmin.Id });
            userContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = usuarioAlunoId, RoleId = roleAluno.Id });
            await userContext.SaveChangesAsync();

            if (alunosContext.Alunos.Any())
                return;

            var aluno = new Aluno(usuarioAlunoId, "Aluno Inteligente");

            alunosContext.Alunos.Add(aluno);
            await alunosContext.SaveChangesAsync();
        }
    }
}
