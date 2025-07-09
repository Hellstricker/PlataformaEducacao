using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacao.Cadastros.Application.Services;
using PlataformaEducacao.Cadastros.Application.ViewModels;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.WebApps.WebApi.Enums;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{
    [Authorize(Roles = nameof(PerfilUsuarioEnum.ADMIN))]
    public class CursosController : BaseController
    {
        private readonly ICursoApplicationService _cursoApplicationService;

        public CursosController(ICursoApplicationService cursoApplicationService,
            IDomainNotificationHandler domainNotificationHandler,
            IMediatorHandler mediatorHandler,
            IUser loggedUser
            )
            :base(domainNotificationHandler, mediatorHandler, loggedUser)
        {
            _cursoApplicationService = cursoApplicationService;
        }

        [Authorize(Roles = nameof(PerfilUsuarioEnum.ADMIN))]
        [HttpPost()]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CursoViewModel cursoDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _cursoApplicationService.AdicionarCurso(cursoDto);

            return CustomResponse($"Curso {cursoDto.Titulo} cadastrado com sucesso.");
        }

        [Authorize(Roles = nameof(PerfilUsuarioEnum.ADMIN))]
        [HttpPost("{id:guid}/cadastrar-aula")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromRoute] Guid id, [FromBody] AulaViewModel aulaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);            
            aulaDto.CursoId = id;

            await _cursoApplicationService.AdicionarAula(aulaDto);
            
            return CustomResponse("Aula cadastrada com sucesso.");
        }
    }
}
