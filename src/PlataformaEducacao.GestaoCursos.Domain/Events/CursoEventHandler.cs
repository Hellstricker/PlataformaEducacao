using MediatR;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Messages.Messages.IntegrationEvents;
using PlataformaEducacao.GestaoCursos.Domain.Interfaces;

namespace PlataformaEducacao.GestaoCursos.Domain.Events
{
    public class CursoEventHandler
    {
        private readonly IGestaoCursosDomainService _gestaoCursosDomainService;
        private readonly IMediatorHandler _mediator;
        private readonly ICursoRepository _cursoRepository;

        public CursoEventHandler(
            IGestaoCursosDomainService gestaoCursosDomainService,
            IMediatorHandler mediator,
            ICursoRepository cursoRepository)
        {
            _gestaoCursosDomainService = gestaoCursosDomainService;
            _mediator = mediator;
            _cursoRepository = cursoRepository;
        }
    }
}
