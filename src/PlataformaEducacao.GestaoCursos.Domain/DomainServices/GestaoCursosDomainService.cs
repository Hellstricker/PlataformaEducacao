using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Core.Messages.Messages.IntegrationEvents;
using PlataformaEducacao.Core.Messages.Messages.Notifications;
using PlataformaEducacao.GestaoCursos.Domain.Interfaces;

namespace PlataformaEducacao.GestaoCursos.Domain.DomainServices
{
    public class GestaoCursosDomainService : DomainService, IGestaoCursosDomainService
    {
        private readonly ICursoRepository _cursoRepository;
        
        public GestaoCursosDomainService(ICursoRepository cursoRepository, IMediatorHandler mediator)
            :base(mediator)
        {
            _cursoRepository = cursoRepository;        
        }
        public async Task CadastrarCurso(Curso curso)
        {
            if (!ValidarEntidade(curso)) return;
            _cursoRepository.Adicionar(curso);            
            if(await _cursoRepository.UnitOfWork.CommitAsync())
                await _mediator.PublicarEvento(new CursoCadastradoEvent(curso.Id, curso.Nome, curso.ConteudoProgramatico.Objetivo, curso.ConteudoProgramatico.Conteudo));            
        }

        public async Task<bool> CadastrarAula(Guid cursoId, Aula aula)
        {               
            var curso = await _cursoRepository.ObterCursoComAulasPorIdAsync(cursoId);
            if (curso is null) throw new DomainException("Curso não encontrado");                         
            curso.AdicionarAula(aula);
            if (!ValidarEntidade(aula)) return false;
            _cursoRepository.AdicionarAula(aula);            
            if(await _cursoRepository.UnitOfWork.CommitAsync())
                await _mediator.PublicarEvento(new AulaCadastradaEvent(aula.CursoId, aula.Id, aula.Titulo, aula.Conteudo, aula.TotalMinutos));
            return true;
        }
    }
}
