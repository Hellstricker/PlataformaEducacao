using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Messages.Notifications;
using PlataformaEducacao.GestaoAlunos.Application.Services;
using PlataformaEducacao.WebApps.WebApi.Contexts;
using PlataformaEducacao.WebApps.WebApi.Enums;
using PlataformaEducacao.WebApps.WebApi.Extensions.Jwts;
using PlataformaEducacao.WebApps.WebApi.ViewModels;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiController]       
    public class AutenticacaoController : MainSignInController
    {
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;
        private readonly IGestaoAlunosApplicationService _gestaoAlunosApplicationService;
        public AutenticacaoController(
            SignInManager<IdentityUser<Guid>> signInManager,
            UserManager<IdentityUser<Guid>> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IOptions<JwtSettings> jwtSettings,
            ApplicationDbContext context,
            IDomainNotificationHandler notificador,
            IMapper mapper,
            IGestaoAlunosApplicationService gestaoAlunosApplicationService,
            IUser appUser,
            IMediatorHandler mediator)
            : base(userManager,roleManager, jwtSettings, context, notificador, mapper, appUser, mediator)
        {
            _signInManager = signInManager;
            _gestaoAlunosApplicationService = gestaoAlunosApplicationService;
        }


        [HttpPost("login")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(LoginResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user == null) return CustomResponse("Usuário ou senha incorretos, tente novamente");

            var result = await _signInManager.PasswordSignInAsync(user, model.Senha!, false, true);

            if (!result.Succeeded) return CustomResponse("Usuário ou senha incorretos, tente novamente");

            if (result.IsLockedOut) return CustomResponse("Usuário bloqueado por tentativas inválidas");

            return CustomResponse(GetJwt(user.Email!).Result);
        }


        [HttpPost("cadastrar-aluno")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CadastrarUsuarioViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CadastrarAlunoAsync(CadastrarUsuarioViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser<Guid>()
            {
                UserName = model.Nome,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Senha!);

            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    NotificarErro(this.GetType().ToString(), error.Description);
            else
            {
                var roleExist = await _roleManager.RoleExistsAsync(nameof(PerfilUsuario.ALUNO));
                if (!roleExist)                
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(nameof(PerfilUsuario.ALUNO)));                

                await _userManager.AddToRoleAsync(user, nameof(PerfilUsuario.ALUNO));

                await _gestaoAlunosApplicationService.CadastrarAluno(Guid.Parse(await _userManager.GetUserIdAsync(user)), model.Nome);
               
            }

            return CustomResponse(model);
        }

        [HttpPost("cadastrar-administrador")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CadastrarUsuarioViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CadastrarAdministradorAsync(CadastrarUsuarioViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser<Guid>()
            {
                UserName = model.Nome,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Senha!);

            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    NotificarErro(this.GetType().ToString(), error.Description);
            else
            {
                var roleExist = await _roleManager.RoleExistsAsync(nameof(PerfilUsuario.ADMIN));
                if (!roleExist)                
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(nameof(PerfilUsuario.ADMIN)));                

                await _userManager.AddToRoleAsync(user, nameof(PerfilUsuario.ADMIN));
            }

            return CustomResponse(model);
        }

    }
}
