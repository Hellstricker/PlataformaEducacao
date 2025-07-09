using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.DomainObjects.Dtos;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.Gestao.Application.Commands;
using PlataformaEducacao.Gestao.Application.Queries;
using PlataformaEducacao.Gestao.Domain;
using PlataformaEducacao.Pagamentos.Business;
using PlataformaEducacao.WebApps.WebApi.Enums;
using PlataformaEducacao.WebApps.WebApi.ViewModels;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{

    [Authorize(Roles = nameof(PerfilUsuarioEnum.ALUNO))]
    public class AlunosController : BaseController
    {
        private readonly IPagamentoService _pagamentoService;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IAlunoQueries _alunoQueries;

        public AlunosController(
            IDomainNotificationHandler notifications, 
            IMediatorHandler mediatorHandler, 
            IUser loggedUser,
            IPagamentoService pagamentoService,
            IAlunoRepository alunoRepository,
            IAlunoQueries alunoQueries) : base(notifications, mediatorHandler, loggedUser)
        {
            _pagamentoService = pagamentoService;
            _alunoRepository = alunoRepository; 
            _alunoQueries = alunoQueries;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastrarAlunoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var command = new CadastrarAlunoCommand(model.Nome, model.Email,model.Senha, model.ConfirmarSenha);            
            await _mediatorHandler.PublicarCommand(command);
            return CustomResponse();
        }

        
        [HttpPost("{id:guid}/matricular")]
        public async Task<IActionResult> PostMatricular(Guid id, [FromBody] MatricularAlunoViewModel model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if (id != AlunoId) return BadRequest("Apenas o próprio aluno pode se cadastrar.");            
            var command = new MatricularAlunoCommand(AlunoId, model.CursoId, model.CursoNome, model.CursoValor, model.CursoTotalAulas);
            await _mediatorHandler.PublicarCommand(command);
            return CustomResponse();
        }

        [HttpPost("{id:guid}/pagar-matricula-curso")]
        public async Task<IActionResult> PostPagarMatricula(Guid id, [FromBody] PagamentoMatriculaViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != AlunoId) return BadRequest("Apenas o próprio aluno pode se matricular.");
            var matricula = await _alunoRepository.ObterMatriculaParaPagamento(id, model.CursoId);
            if(matricula == null) return NotFound("Matricula não encontrada ou não está pendente de pagamento.");
            await _pagamentoService.RealizarPagamentoCurso(new PagamentoMatriculaDto(matricula.Id, matricula.Curso.CursoId, matricula.AlunoId, matricula.Curso.CursoValor, model.NomeCartao!, model.NumeroCartao!, model.MesAnoExpiracao!, model.Ccv!));            
            return CustomResponse();
        }

        [HttpPost("{id:guid}/finalizar-aula")]
        public async Task<IActionResult> PostFinalizarAula(Guid id, [FromBody] FinalizarAulaViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != AlunoId) return BadRequest("Apenas o próprio aluno pode finalizar suas aulas.");
            var command = new AlunoFinalizarAulaCommand(AlunoId, model.CursoId, model.AulaId);
            await _mediatorHandler.PublicarCommand(command);
            return CustomResponse();
        }

        [HttpGet("{id:guid}/minhas-matriculas")]
        public async Task<IActionResult> GetObterMatriculas(Guid id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != AlunoId) return BadRequest("Apenas o próprio aluno pode ver suas matriculas.");
            return Ok(await _alunoQueries.ObterMatriculasAluno(AlunoId));
        }
    }
}
