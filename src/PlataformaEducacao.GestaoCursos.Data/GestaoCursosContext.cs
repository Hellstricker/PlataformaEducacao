using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Data;
using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.GestaoCursos.Domain;
using PlataformaEducacao.Core.Extensions;

namespace PlataformaEducacao.GestaoCursos.Data
{
    public class GestaoCursosContext : DbContext, IUnityOfWork
    {
        private readonly IMediatorHandler _mediator;

        public GestaoCursosContext(DbContextOptions<GestaoCursosContext> options, IMediatorHandler mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aula> Aulas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoCursosContext).Assembly);

            base.OnModelCreating(modelBuilder);

        }

        public async Task<bool> CommitAsync()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataAtualizacao") != null))
            {
                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                }
            }

            var saved = await base.SaveChangesAsync() > 0;
            if (saved) await _mediator.PublicarEventos(this);
            return saved;
        }
    }
}
