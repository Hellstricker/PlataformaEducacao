using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.Core.Extensions
{
    public static class MediatorExtension
    {

        public static async Task PublicarEventos(this IMediatorHandler mediator, DbContext context)
        {
            var a = context.ChangeTracker
                .Entries<Entity>();

            foreach (var s in a)
            {
                Console.WriteLine(s.Entity.Notificacoes.Any());
            }

            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any())
                .Select(x => x.Entity);

            var domainEvents = domainEntities
                .SelectMany(x => x.Notificacoes)
                .ToList();

            domainEntities.ToList().ForEach(x => x.LimparEventos());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublicarEvento(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}