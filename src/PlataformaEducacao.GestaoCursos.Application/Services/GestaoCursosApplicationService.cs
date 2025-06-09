using AutoMapper;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.GestaoCursos.Application.Dtos;
using PlataformaEducacao.GestaoCursos.Domain;
using PlataformaEducacao.GestaoCursos.Domain.Interfaces;

namespace PlataformaEducacao.GestaoCursos.Application.Services
{
    public class GestaoCursosApplicationService : IGestaoCursosApplicationService
    {
        private readonly ICursoRepository _repository;
        private readonly IGestaoCursosDomainService _gestaoCursosDomainService;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;

        public GestaoCursosApplicationService(ICursoRepository repository, IMapper mapper, IGestaoCursosDomainService gestaoCursosDomainService, IMediatorHandler mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _gestaoCursosDomainService = gestaoCursosDomainService;
            _mediator = mediator;
        }

        public async Task<CursoDto> ObterCursoPorId(Guid cursoId)
        {
            return _mapper.Map<CursoDto>(await _repository.ObterCursoPorIdAsync(cursoId));
        }

        public async Task<CursoDto> ObterCursoCompletoSemTrackingPorIdAsync(Guid cursoId)
        {
            return _mapper.Map<CursoDto>(await _repository.ObterCursoCompletoSemTrackingPorIdAsync(cursoId));
        }

        public async Task CadastrarCurso(CadastrarCursoDto cursoDto)
        {

            await _gestaoCursosDomainService.CadastrarCurso(_mapper.Map<Curso>(cursoDto));
        }

        public async Task CadastrarAula(Guid cursoId, CadastrarAulaDto aulaDto)
        {
            await _gestaoCursosDomainService.CadastrarAula(cursoId, _mapper.Map<Aula>(aulaDto));
        }

        public async Task<IEnumerable<CursoDto>> ObterTodosCursos()
        {
            return _mapper.Map<IEnumerable<CursoDto>>( await _repository.ObterTodosAsync());    
        }
    }
}
