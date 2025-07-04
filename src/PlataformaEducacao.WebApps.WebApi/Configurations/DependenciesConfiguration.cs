using MediatR;
using Microsoft.AspNetCore.Identity;
using PlataformaEducacao.Cadastros.Application.Services;
using PlataformaEducacao.Cadastros.Data;
using PlataformaEducacao.Cadastros.Data.Repositories;
using PlataformaEducacao.Cadastros.Domain;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.IntegrationEvents;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.Gestao.Application.Commands;
using PlataformaEducacao.Gestao.Data;
using PlataformaEducacao.Gestao.Data.Repositories;
using PlataformaEducacao.Gestao.Domain;
using PlataformaEducacao.WebApps.WebApi.Contexts;
using PlataformaEducacao.WebApps.WebApi.Events;
using PlataformaEducacao.WebApps.WebApi.Extensions;
using PlataformaEducacao.WebApps.WebApi.Extensions.Identity;

namespace PlataformaEducacao.WebApps.WebApi.Configurations
{
    public static class DependenciesConfiguration
    {
        public static WebApplicationBuilder ResolveDependencies(this WebApplicationBuilder builder)
        {
            //Gestao            
            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
            builder.Services.AddScoped<GestaoContext>();
            builder.Services.AddScoped<AlunoCommandHandler>();

            //Cursos
            builder.Services.AddScoped<ICursoApplicationService, CursoApplicationService>();
            builder.Services.AddScoped<ICursoRepository, CursoRepository>();
            builder.Services.AddScoped<CursoContext>();  

            //Mediator
            builder.Services.AddScoped<IMediator, Mediator>();
            builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Notificações
            builder.Services.AddScoped<DomainNotificationHandler>();
            builder.Services.AddScoped<IDomainNotificationHandler>(sp => sp.GetRequiredService<DomainNotificationHandler>());
            builder.Services.AddScoped<INotificationHandler<DomainNotification>>(sp => sp.GetRequiredService<DomainNotificationHandler>());

            //Identity
            builder.Services.AddScoped<ApplicationDbContext>();
            //builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IUser, AspNetUser>();
            builder.Services.AddScoped<ILookupNormalizer, CustomLookupNormalizer>();

            //Commands
            builder.Services.AddScoped<IRequestHandler<CadastrarAlunoCommand, bool>, AlunoCommandHandler>();
            builder.Services.AddScoped<IRequestHandler<MatricularAlunoCommand, bool>, AlunoCommandHandler>();

            //Events
            builder.Services.AddScoped<INotificationHandler<AlunoCadastradoEvent>, UsuarioEventHandler>();

            return builder;
        }
    }
}
