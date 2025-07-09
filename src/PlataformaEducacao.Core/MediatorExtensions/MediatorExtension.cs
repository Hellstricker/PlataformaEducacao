using Microsoft.EntityFrameworkCore;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.Core.MediatorExtensions
{
    public static class MediatorExtension
    {

        public static async Task PublicarEventos(this IMediatorHandler mediator, DbContext context)
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any())
                .Select(x => x.Entity);

            var events = domainEntities
                .SelectMany(x => x.Notificacoes)
                .ToList();

            domainEntities.ToList().ForEach(x => x.LimparEventos());

            var tasks = events
                .Select(async (evento) =>
                {
                    await mediator.PublicarEvento(evento);
                });

            await Task.WhenAll(tasks);
        }
    }

}
