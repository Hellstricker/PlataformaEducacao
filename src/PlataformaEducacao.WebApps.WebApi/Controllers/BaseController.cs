using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.WebApps.WebApi.ViewModels;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly IDomainNotificationHandler _notifications;
        protected readonly IMediatorHandler _mediatorHandler;
        protected Guid AlunoId = Guid.Empty;

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

        protected ActionResult CustomResponse(object? result = null, bool? isPost = false)
        {
            if (!OperacaoValida())
            {
                return BadRequest(new
                {
                    Success = false,
                    Errors = _notifications.ObterNotificacoes().Select(n => n.Value)
                });
            }

            return Ok(new
                BaseResultViewModel
            {
                Success = true,
                Data = result
            });
        }
    }
}
