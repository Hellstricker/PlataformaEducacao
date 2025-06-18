using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Messages.Notifications;
using PlataformaEducacao.GestaoAlunos.Application.Services;
using PlataformaEducacao.GestaoCursos.Application.Dtos;
using PlataformaEducacao.GestaoCursos.Application.Services;
using PlataformaEducacao.Pagamentos.Business;
using PlataformaEducacao.WebApps.WebApi.Enums;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{
    public class CursoController : BaseController
    {
        private readonly IGestaoCursosApplicationService _gestaoCursosApplicationService;

        public CursoController(
                IDomainNotificationHandler notifications,
                IMediatorHandler mediatorHandler,
                IGestaoCursosApplicationService gestaoCursosApplicationService,
                IGestaoAlunosApplicationService gestaoAlunosApplicationService,
                IPagamentoService pagamentoService,
                IUser loggedUser)
            : base(notifications, mediatorHandler, loggedUser)
        {
            _gestaoCursosApplicationService = gestaoCursosApplicationService;        
        }
        [Authorize(Roles = nameof(PerfilUsuario.ADMIN))]
        [HttpPost()]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CadastrarCursoDto cursoDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _gestaoCursosApplicationService.CadastrarCurso(cursoDto);
            return CustomResponse($"Curso cadastrado com sucesso.");
        }

        [Authorize(Roles = nameof(PerfilUsuario.ADMIN))]
        [HttpPost("{id:guid}/cadastrar-aula")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromRoute] Guid id, [FromBody] CadastrarAulaDto aulaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _gestaoCursosApplicationService.CadastrarAula(id, aulaDto);
            return CustomResponse("Aula cadastrada com sucesso.");
        }

        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<CursoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCursos() { 
            var cursos =  await _gestaoCursosApplicationService.ObterTodosCursos();
            return CustomResponse(cursos);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CursoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCursos(Guid id)
        {
            var curso = await _gestaoCursosApplicationService.ObterCursoCompletoSemTrackingPorIdAsync(id);
            return CustomResponse(curso);
        }
    }
}