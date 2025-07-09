using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Data;
using PlataformaEducacao.Core.MediatorExtensions;
using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Pagamentos.Business;

namespace PlataformaEducacao.Pagamentos.Data
{
    public class PagamentosContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediator;

        public PagamentosContext(DbContextOptions<PagamentosContext> options, IMediatorHandler mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        public DbSet<Pagamento> Pagamentos { get; set; }

        public DbSet<Transacao> Transacoes { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentosContext).Assembly);

            base.OnModelCreating(modelBuilder);

        }
    }
}
