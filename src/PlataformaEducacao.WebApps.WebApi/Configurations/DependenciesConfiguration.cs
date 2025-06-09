using MediatR;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages.Messages.IntegrationEvents;
using PlataformaEducacao.Core.Messages.Messages.Notifications;
using PlataformaEducacao.GestaoAlunos.Application.Services;
using PlataformaEducacao.GestaoAlunos.Data;
using PlataformaEducacao.GestaoAlunos.Data.Repositories;
using PlataformaEducacao.GestaoAlunos.Domain.DomainServices;
using PlataformaEducacao.GestaoAlunos.Domain.Events;
using PlataformaEducacao.GestaoAlunos.Domain.Interfaces;
using PlataformaEducacao.GestaoCursos.Application.Services;
using PlataformaEducacao.GestaoCursos.Data;
using PlataformaEducacao.GestaoCursos.Data.Repositories;
using PlataformaEducacao.GestaoCursos.Domain.DomainServices;
using PlataformaEducacao.GestaoCursos.Domain.Interfaces;
using PlataformaEducacao.Pagamentos.AntiCorruption;
using PlataformaEducacao.Pagamentos.Business;
using PlataformaEducacao.Pagamentos.Data;
using PlataformaEducacao.Pagamentos.Data.Repositories;
using PlataformaEducacao.WebApps.WebApi.Contexts;
using PlataformaEducacao.WebApps.WebApi.Extensions.Identity;

namespace PlataformaEducacao.WebApps.WebApi.Configurations
{

    public static class DependenciesConfiguration
    {
        public static WebApplicationBuilder ResolveDependencies(this WebApplicationBuilder builder)
        {
            //Injeção de Dependência
            //Gestao de Cursos
            builder.Services.AddScoped<ICursoRepository, CursoRepositiry>();
            builder.Services.AddScoped<IGestaoCursosApplicationService, GestaoCursosApplicationService>();
            builder.Services.AddScoped<IGestaoCursosDomainService, GestaoCursosDomainService>();
            builder.Services.AddScoped<GestaoCursosContext>();

            //Gestao de Alunos
            builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
            builder.Services.AddScoped<IGestaoAlunosApplicationService, GestaoAlunosApplicationService>();
            builder.Services.AddScoped<IGestaoAlunosDomainService, GestaoAlunosDomainService>();
            builder.Services.AddScoped<GestaoAlunosContext>();            
            builder.Services.AddScoped<INotificationHandler<PagamentoRealizadoEvent>, AlunoEventHandler>();            
            builder.Services.AddScoped<INotificationHandler<ProgressoAtualizadoEvent>, AlunoEventHandler>();
            builder.Services.AddScoped<INotificationHandler<CursoFinalizadoEvent>, AlunoEventHandler>();
            builder.Services.AddScoped<INotificationHandler<AulaFinalizadaEvent>, AlunoEventHandler>();            

            //Pagamentos
            builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
            builder.Services.AddScoped<IPagamentoService, PagamentoService>();
            builder.Services.AddScoped<IPagamentoCartaoCreditoFacade, PagamentoCartaoCreetitoFacade>();
            builder.Services.AddScoped<IPaypalGateway, PaypalGateway>();
            builder.Services.AddScoped<Pagamentos.AntiCorruption.IConfigurationManager, Pagamentos.AntiCorruption.ConfigurationManager>();
            builder.Services.AddScoped<PagamentosContext>();

            //Mediator
            builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
            builder.Services.AddScoped<IMediator, Mediator>();

            //Notificações
            builder.Services.AddScoped<DomainNotificationHandler>();
            builder.Services.AddScoped<IDomainNotificationHandler>(sp => sp.GetRequiredService<DomainNotificationHandler>());
            builder.Services.AddScoped<INotificationHandler<DomainNotification>>(sp => sp.GetRequiredService<DomainNotificationHandler>());

            //Identity
            builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IUser, AspNetUser>();


            return builder;
        }
    }
}
