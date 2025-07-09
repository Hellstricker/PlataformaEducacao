using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Cadastros.Data;
using PlataformaEducacao.Cadastros.Domain;
using PlataformaEducacao.Gestao.Data;
using PlataformaEducacao.Gestao.Domain;
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
            var cursosContext = scope.ServiceProvider.GetRequiredService<CursoContext>();
            var gestaoContext = scope.ServiceProvider.GetRequiredService<GestaoContext>();
            var pagamentosContext = scope.ServiceProvider.GetRequiredService<PagamentosContext>();

            if(environment.IsDevelopment())
            {
                await userContext.Database.MigrateAsync();
                await cursosContext.Database.MigrateAsync();
                await gestaoContext.Database.MigrateAsync();
                await pagamentosContext.Database.MigrateAsync();

                await EnsureSeed(userContext, cursosContext, gestaoContext);//, alunosContext, cursosContext);
            }
        }

        private static async Task EnsureSeed(ApplicationDbContext userContext, CursoContext cursosContext, GestaoContext gestaoContext)//, GestaoAlunosContext alunosContext)
        {
            if (cursosContext.Cursos.Any())
                return;

            var curso = new Curso("Como criar uma plataforma de ensino", 1500.35M, new ConteudoProgramatico("Ensinar pessoas a criar uma plataforma de ensino", 350));
            var cursoId = curso.Id;
            cursosContext.Cursos.Add(curso);
            await cursosContext.SaveChangesAsync();

            if (cursosContext.Aulas.Any())
                return;

            var aula1 = new Aula("Introdução ao DDD",55, "Nesta aula vamos aprender o que é DDD e como aplicá-lo em nossos projetos.");
            
            var aula2 = new Aula("Introdução ao TDD", 45, "Nesta aula vamos aprender o que é TDD e como aplicá-lo em nossos projetos.");
            var aula3 = new Aula("Introdução ao ASP.NET Core", 60, "Nesta aula vamos aprender o que é ASP.NET Core e como aplicá-lo em nossos projetos.");
            var aula4 = new Aula("Introdução ao Entity Framework Core", 50, "Nesta aula vamos aprender o que é Entity Framework Core e como aplicá-lo em nossos projetos.");
            var aula5 = new Aula("Introdução ao ASP.NET Core Web API", 40, "Nesta aula vamos aprender o que é ASP.NET Core Web API e como aplicá-lo em nossos projetos.");
            var aula6 = new Aula("Introdução ao C#", 30, "Nesta aula vamos aprender o que é C# e como aplicá-lo em nossos projetos.");

            aula1.AssociarCurso(cursoId);
            aula2.AssociarCurso(cursoId);
            aula3.AssociarCurso(cursoId);
            aula4.AssociarCurso(cursoId);
            aula5.AssociarCurso(cursoId);
            aula6.AssociarCurso(cursoId);


            cursosContext.Aulas.AddRange(aula1, aula2, aula3, aula4, aula5, aula6);
            await cursosContext.SaveChangesAsync();
                       

            if (gestaoContext.Alunos.Any())
                return;
            var alunoEmail = "alunointeligente@teste.com";
            var aluno = new Aluno("Aluno Inteligente", alunoEmail);
            var alunoId = aluno.Id; // Associar o Aluno ao usuário criado

            gestaoContext.Alunos.Add(aluno);
            await gestaoContext.SaveChangesAsync();

            if (userContext.Roles.Any())
                return;

            IdentityRole<Guid> roleAdmin = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = nameof(PerfilUsuarioEnum.ADMIN),
                NormalizedName = nameof(PerfilUsuarioEnum.ADMIN)
            };

            IdentityRole<Guid> roleAluno = new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = nameof(PerfilUsuarioEnum.ALUNO),
                NormalizedName = nameof(PerfilUsuarioEnum.ALUNO)
            };
            userContext.Roles.AddRange(roleAdmin, roleAluno);
            await userContext.SaveChangesAsync();

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

            IdentityUser<Guid> usuarioAluno = new IdentityUser<Guid>
            {
                Id = alunoId,
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



            if (userContext.UserRoles.Any())
                return;

            userContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = userAdminId, RoleId = roleAdmin.Id });
            userContext.UserRoles.Add(new IdentityUserRole<Guid> { UserId = alunoId, RoleId = roleAluno.Id });

            await userContext.SaveChangesAsync();
        }
    }
}
