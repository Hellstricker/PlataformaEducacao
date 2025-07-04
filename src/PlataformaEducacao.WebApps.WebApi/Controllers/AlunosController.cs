using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.Gestao.Application.Commands;
using PlataformaEducacao.WebApps.WebApi.ViewModels;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{

    public class AlunosController : BaseController
    {
        public AlunosController(
            IDomainNotificationHandler notifications, 
            IMediatorHandler mediatorHandler, 
            IUser loggedUser) : base(notifications, mediatorHandler, loggedUser)
        {
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
        public async Task<IActionResult> PostMatricula(Guid id, [FromBody] MatricularAlunoViewModel model)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if (id != AlunoId) return BadRequest("Apenas o próprio aluno pode se cadastrar.");            
            var command = new MatricularAlunoCommand(AlunoId, model.CursoId, model.CursoNome, model.CursoValor, model.CursoTotalAulas);
            await _mediatorHandler.PublicarCommand(command);
            return CustomResponse();
        }
    }
}
