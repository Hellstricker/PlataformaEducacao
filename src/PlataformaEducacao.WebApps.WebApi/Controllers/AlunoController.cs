using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.DomainObjects.Dtos;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Messages.Notifications;
using PlataformaEducacao.GestaoAlunos.Application.Dtos;
using PlataformaEducacao.GestaoAlunos.Application.Services;
using PlataformaEducacao.GestaoCursos.Application.Services;
using PlataformaEducacao.Pagamentos.Business;
using PlataformaEducacao.WebApps.WebApi.Enums;
using PlataformaEducacao.WebApps.WebApi.ViewModels;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{    
    public class AlunoController : BaseController
    {
        private readonly IGestaoAlunosApplicationService _gestaoAlunosApplicationService;
        private readonly IGestaoCursosApplicationService _gestaoCursosApplicationService;
        private readonly IPagamentoService _pagamentoService;
        public AlunoController(
            IDomainNotificationHandler notifications,
            IMediatorHandler mediatorHandler,
            IGestaoAlunosApplicationService gestaoAlunosApplicationService,
            IUser loggedUser,
            IGestaoCursosApplicationService gestaoCursosApplicationService,
            IPagamentoService pagamentoService)
            : base(notifications, mediatorHandler, loggedUser)
        {
            _gestaoAlunosApplicationService = gestaoAlunosApplicationService;
            _gestaoCursosApplicationService = gestaoCursosApplicationService;
            _pagamentoService = pagamentoService;
        }

        [HttpGet("todos-alunos")]
        [Authorize(Roles = nameof(PerfilUsuario.ADMIN))]
        [ProducesResponseType(typeof(IEnumerable<AlunoCompletoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAlunos()
        {
            var alunos = await _gestaoAlunosApplicationService.ObterAlunosCompletosSemTracking();
            return CustomResponse(alunos);
        }

        [HttpGet("meus-dados")]
        [Authorize(Roles = nameof(PerfilUsuario.ALUNO))]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAlunoById()
        {
            var aluno = await _gestaoAlunosApplicationService.ObterAlunoPorId(AlunoId);
            if (aluno == null) NotificarErro("AlunoNaoEncontrado", "Aluno não encontrado.");
            return CustomResponse(aluno);
        }

        [Authorize(Roles = nameof(PerfilUsuario.ALUNO))]
        [HttpPost("matricular")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] MatricularAlunoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var curso = await _gestaoCursosApplicationService.ObterCursoPorId(model.CursoId);

            if (curso is not null)
            {
                var matriculaDto = new MatricularAlunoDto
                {
                    CursoId = curso.Id,
                    NomeCurso = curso.Nome
                };
                await _gestaoAlunosApplicationService.MatricularAluno(AlunoId, matriculaDto);
            }
            else
                NotificarErro("CursoNaoEncontrado", "Curso não encontrado.");

            return CustomResponse("Matricula realizada com sucesso.");
        }

        [Authorize(Roles = nameof(PerfilUsuario.ALUNO))]
        [HttpPost("pagar-matricula")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] PagamentoMatriculaViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var matricula = await _gestaoAlunosApplicationService.ObterMatriculaParaPagamentoPorId(model.MatriculaId);


            if (matricula is not null)
            {
                var curso = await _gestaoCursosApplicationService.ObterCursoPorId(matricula.CursoId);
                await _pagamentoService.RealizarPagamentoCurso(
                    new PagamentoMatriculaDto(matricula.Id, AlunoId, curso.Valor,model.NomeCartao!,model.NumeroCartao!,model.MesAnoExpiracao!,model.Ccv!)
                    );
            }
            else
                NotificarErro("CursoNaoEncontrado", "Matricula não encontrada ou não está pendende de pagamento.");

            return CustomResponse("Pagamento realizado com sucesso.");
        }

        [Authorize(Roles = nameof(PerfilUsuario.ALUNO))]
        [HttpPost("finalizar-aula")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] FinalizarAulaViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var curso = await _gestaoCursosApplicationService.ObterCursoCompletoSemTrackingPorIdAsync(model.CursoId);
            if (curso is null)
            {
                NotificarErro("CursoNaoEncontrado", "Curso não encontrado.");
                return CustomResponse();
            }

            var aula = curso.Aulas?.FirstOrDefault(a => a.Id == model.AulaId);
            if (aula is null)
            {
                NotificarErro("CursoNaoEncontrado", "Aula não pertence ao curso informado.");
                return CustomResponse();
            }

            await _gestaoAlunosApplicationService.FinalizarAula(AlunoId, model.CursoId, model.AulaId, curso.Aulas.Count());

            return CustomResponse("Aula finalizada com sucesso.");
        }
    }
}
