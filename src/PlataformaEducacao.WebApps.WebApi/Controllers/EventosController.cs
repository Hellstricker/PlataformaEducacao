using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacao.Core.Data.EventSourcing;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{    
    public class EventosController : Controller
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public EventosController(IEventSourcingRepository eventSourcingRepository)
        {
            _eventSourcingRepository = eventSourcingRepository;
        }

        [HttpGet("eventos/{id:guid}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var eventos = await _eventSourcingRepository.ObterEventos(id);
            return Ok(eventos);
        }
    }
}
