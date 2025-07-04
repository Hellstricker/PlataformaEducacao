using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Cadastros.Domain;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Data;
using PlataformaEducacao.Core.MediatorExtensions;
using PlataformaEducacao.Core.Messages;


namespace PlataformaEducacao.Cadastros.Data
{
    public class CursoContext: DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CursoContext(DbContextOptions<CursoContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Aula> Aulas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();            

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CursoContext).Assembly);

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
            if (saved) await _mediatorHandler.PublicarEventos(this);
            return saved;
        }
    }
}
