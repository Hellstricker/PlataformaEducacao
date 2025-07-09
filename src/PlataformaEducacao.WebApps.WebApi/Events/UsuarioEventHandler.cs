using MediatR;
using Microsoft.AspNetCore.Identity;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Messages.IntegrationEvents;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.Gestao.Application.Events;
using PlataformaEducacao.WebApps.WebApi.Enums;
using PlataformaEducacao.WebApps.WebApi.Extensions.String;

namespace PlataformaEducacao.WebApps.WebApi.Events
{
    public class UsuarioEventHandler:INotificationHandler<AlunoCadastroRealizadoEvent>
    {
        protected readonly UserManager<IdentityUser<Guid>> _userManager;
        protected readonly IMediatorHandler _mediatorHandler;
        protected readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UsuarioEventHandler(
            UserManager<IdentityUser<Guid>> userManager,
            IMediatorHandler mediatorHandler,
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _mediatorHandler = mediatorHandler;
            _roleManager = roleManager;
        }

        public async Task Handle(AlunoCadastroRealizadoEvent notification, CancellationToken cancellationToken)
        {

            var user = new IdentityUser<Guid>()
            {
                UserName = notification.Nome.NormalizedToIdentityUserName(),
                Email = notification.Email,
                EmailConfirmed = true
            };
            
            user.Id = notification.AlunoId;

            var result = await _userManager.CreateAsync(user, notification.Senha);

            if (!result.Succeeded)
                foreach (var error in result.Errors)
                    await _mediatorHandler.PublicarNotificacao(new DomainNotification(this.GetType().ToString(), error.Description));            
            else
            {
                var roleExist = await _roleManager.RoleExistsAsync(nameof(PerfilUsuarioEnum.ALUNO));
                if (!roleExist)
                    await _roleManager.CreateAsync(new IdentityRole<Guid>(nameof(PerfilUsuarioEnum.ALUNO)));

                await _userManager.AddToRoleAsync(user, nameof(PerfilUsuarioEnum.ALUNO));
            }
        }
    }
}
