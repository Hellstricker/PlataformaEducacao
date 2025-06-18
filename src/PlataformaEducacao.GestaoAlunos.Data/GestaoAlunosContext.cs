using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Data;
using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.GestaoAlunos.Domain;
using PlataformaEducacao.Core.Extensions;
using FluentValidation.Results;

namespace PlataformaEducacao.GestaoAlunos.Data
{
    public class GestaoAlunosContext : DbContext, IUnityOfWork
    {
        private readonly IMediatorHandler _mediator;

        public GestaoAlunosContext(DbContextOptions<GestaoAlunosContext> options, IMediatorHandler mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<AulaFinalizada> AulasFinalizadas { get; set; }
        public DbSet<Certificado> Certificados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");
            
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(DateTime))))
                property.SetColumnType("datetime");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GestaoAlunosContext).Assembly);

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

