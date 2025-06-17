using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Data.EventSourcing;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Messages.Notifications;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{


    [AllowAnonymous]
    public class EventosController : BaseController
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;        
        public EventosController(
            IEventSourcingRepository eventSourcingRepository, 
            IDomainNotificationHandler domainNotificationHandler, 
            IMediatorHandler mediatorHandler, 
            IUser loggedUser)
            :base(domainNotificationHandler, mediatorHandler, loggedUser)
        {
            _eventSourcingRepository = eventSourcingRepository;
        }
        [HttpGet("obter-eventos/{id:guid}")]
        public async Task<IActionResult> ObterEventos(Guid id)
        {
            //var a = await _eventSourcingRepository.ObterEventos2(id);

            var eventos = await _eventSourcingRepository.ObterEventos(id);
            return Ok(eventos);
        }
    }
}
