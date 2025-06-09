using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Messages.Notifications;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly IDomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediatorHandler;
        protected Guid AlunoId = Guid.Empty; //Guid.Parse("00000000-0000-0000-0000-000000000001");

        public BaseController(IDomainNotificationHandler notifications, IMediatorHandler mediatorHandler, IUser loggedUser)
        {
            _notifications = notifications;
            _mediatorHandler = mediatorHandler;
            if (loggedUser.IsAuthenticated() && loggedUser.IsInRole("ALUNO"))
                AlunoId = loggedUser.GetId();
        }

        

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacoes();
        }

        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }

        protected ActionResult CustomResponse(object? result = null, bool? isCreation = false)
        {
            if (OperacaoValida())
            {                
                return Ok(new
                {
                    success = true,
                    data = result
                });

            }

            return BadRequest(new
            {
                success = false,
                errors = _notifications.ObterNotificacoes().Select(n => n.Value)
            });
        }

    }
}
