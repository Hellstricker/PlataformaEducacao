using AutoMapper;
using PlataformaEducacao.GestaoAlunos.Application.Dtos;
using PlataformaEducacao.GestaoAlunos.Domain.Interfaces;

namespace PlataformaEducacao.GestaoAlunos.Application.Services
{
    public class GestaoAlunosApplicationService : IGestaoAlunosApplicationService
    {
        private readonly IGestaoAlunosDomainService _gestaoAlunosDomainService;
        private readonly IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;

        public GestaoAlunosApplicationService(IGestaoAlunosDomainService gestaoAlunosDomainService, IAlunoRepository alunoRepository, IMapper mapper)
        {
            _gestaoAlunosDomainService = gestaoAlunosDomainService;
            _alunoRepository = alunoRepository;
            _mapper = mapper;
        }
        public async Task<AlunoCompletoDto> ObterAlunoPorId(Guid alunoId)
        {
            return _mapper.Map<AlunoCompletoDto>(await _alunoRepository.ObterAlunoCompletoSemTrackingPorId(alunoId));
        }
        public async Task MatricularAluno(Guid alunoId, MatricularAlunoDto matriulaDto)
        {
            await _gestaoAlunosDomainService.MatricularAluno(alunoId, matriulaDto.CursoId, matriulaDto.NomeCurso);
        }

        public async Task<MatriculaParaPagamentoDto> ObterMatriculaParaPagamentoPorId(Guid matriculaId)
        {
            return _mapper.Map<MatriculaParaPagamentoDto>(await _alunoRepository.ObterMatriculaPendentePagamento(matriculaId));
        }

        public async Task<IEnumerable<MatriculaParaPagamentoDto>> ObterMatriculasPendentesPagamento(Guid alunoId)
        {
            return _mapper.Map<IEnumerable<MatriculaParaPagamentoDto>>(await _alunoRepository.ObterMatriculasPendentesPagamento(alunoId));
        }

        public async Task FinalizarAula(Guid alunoId, Guid cursoId, Guid aulaId, int totalAulasCurso)
        {
            await _gestaoAlunosDomainService.FinalizarAula(alunoId, cursoId, aulaId, totalAulasCurso);
        }

        public async Task CadastrarAluno(Guid id, string nome)
        {
            await _gestaoAlunosDomainService.CadastrarAluno(id, nome);
        }

        public async Task<IEnumerable<AlunoCompletoDto>> ObterAlunosCompletosSemTracking()
        {
            return _mapper.Map<IEnumerable<AlunoCompletoDto>>(await _alunoRepository.ObterAlunosCompletosSemTracking());
        }
    }
}
