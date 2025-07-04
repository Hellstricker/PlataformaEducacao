using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.WebApps.WebApi.Contexts;
using PlataformaEducacao.WebApps.WebApi.Enums;
using PlataformaEducacao.WebApps.WebApi.Extensions;
using PlataformaEducacao.WebApps.WebApi.ViewModels;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{
    [AllowAnonymous]
    public class AutenticacaoController : MainSignInController
    {
        private readonly SignInManager<IdentityUser<Guid>> _signInManager;

        public AutenticacaoController(
            IDomainNotificationHandler notifications,
            IMediatorHandler mediatorHandler,
            IUser loggedUser,
            SignInManager<IdentityUser<Guid>> signInManager,
            UserManager<IdentityUser<Guid>> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IOptions<JwtSettings> jwtSettings,
            ApplicationDbContext context)
            : base(notifications, mediatorHandler, loggedUser, userManager, roleManager, jwtSettings, context)
        {
            _signInManager = signInManager;
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
                var roleExist = await _roleManager.RoleExistsAsync(nameof(PerfilUsuarioEnum.ADMIN));
                if (!roleExist)
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(nameof(PerfilUsuarioEnum.ADMIN)));

                await _userManager.AddToRoleAsync(user, nameof(PerfilUsuarioEnum.ADMIN));
            }

            return CustomResponse(model);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(LoginResponseViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user == null) NotificarErro(this.GetType().ToString(), "Usuário ou senha incorretos, tente novamente");

            LoginResponseViewModel? loginResponse = null;

            if (OperacaoValida())
            {
                var result = await _signInManager.PasswordSignInAsync(user!, model.Senha!, false, true);

                if (!result.Succeeded) NotificarErro(this.GetType().ToString(), "Usuário ou senha incorretos, tente novamente");

                if (result.IsLockedOut) NotificarErro(this.GetType().ToString(), "Usuário bloqueado por tentativas inválidas");

                loginResponse = await GetJwt(user!.Email!);
            }
            return CustomResponse(loginResponse);
        }
    }
}

