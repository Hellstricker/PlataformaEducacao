using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Core.Messages.Messages.IntegrationEvents;
using PlataformaEducacao.GestaoCursos.Domain.Interfaces;

namespace PlataformaEducacao.GestaoCursos.Domain.DomainServices
{
    public class GestaoCursosDomainService : IGestaoCursosDomainService
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly IMediatorHandler _mediator;
        public GestaoCursosDomainService(ICursoRepository cursoRepository, IMediatorHandler mediator)
        {
            _cursoRepository = cursoRepository;
            _mediator = mediator;
        }
        public async Task CadastrarCurso(Curso curso)
        {
            _cursoRepository.Adicionar(curso);            
            if(await _cursoRepository.UnitOfWork.CommitAsync())
                await _mediator.PublicarEvento(new CursoCadastradoEvent(curso.Id, curso.Nome, curso.ConteudoProgramatico.Objetivo, curso.ConteudoProgramatico.Conteudo));
        }

        public async Task CadastrarAula(Guid cursoId, Aula aula)
        {
            var curso = await _cursoRepository.ObterCursoComAulasPorIdAsync(cursoId);
            if (curso is null) throw new DomainException("Curso não encontrado");
            curso.AdicionarAula(aula);            
            _cursoRepository.AdicionarAula(aula);            
            if(await _cursoRepository.UnitOfWork.CommitAsync())
                await _mediator.PublicarEvento(new AulaCadastradaEvent(aula.CursoId, aula.Id, aula.Titulo, aula.Conteudo, aula.TotalMinutos));
        }
    }
}
