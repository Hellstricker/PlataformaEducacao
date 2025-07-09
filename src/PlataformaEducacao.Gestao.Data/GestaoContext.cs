using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Data;
using PlataformaEducacao.Core.MediatorExtensions;
using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Gestao.Domain;

namespace PlataformaEducacao.Gestao.Data
{
    public class GestaoContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public GestaoContext(DbContextOptions<GestaoContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Certificado> Certificados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<CursoMatriculado>();
            modelBuilder.Ignore<HistoricoAprendizado>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoContext).Assembly);

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

            var saved = await base.SaveChangesAsync() > 0;
            if (saved) await _mediatorHandler.PublicarEventos(this);
            return saved;
        }
    }
}
