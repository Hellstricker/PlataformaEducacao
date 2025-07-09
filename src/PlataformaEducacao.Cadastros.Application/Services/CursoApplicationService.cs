using MediatR;
using PlataformaEducacao.Cadastros.Application.ViewModels;
using PlataformaEducacao.Cadastros.Domain;
using PlataformaEducacao.Cadastros.Domain.Events;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.Core.ViewModelValidationHelpers;

namespace PlataformaEducacao.Cadastros.Application.Services
{
    public class CursoApplicationService : ICursoApplicationService
    {
        public const string CursoNaoEncontrado = "Curso não encontrado.";

        private readonly ICursoRepository _cursoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public CursoApplicationService(ICursoRepository cursoRepository, IMediatorHandler mediator)
        {
            _cursoRepository = cursoRepository;
            _mediatorHandler = mediator;
        }

        public async Task<bool> AdicionarCurso(CursoViewModel cursoViewModel)
        {
            if (!await ModelValido(cursoViewModel)) return false;

            var curso = new Curso(cursoViewModel.Titulo!, cursoViewModel.Valor!, new ConteudoProgramatico(cursoViewModel.Descricao!, cursoViewModel.CargaHoraria!));

            _cursoRepository.Adicionar(curso);

            curso.AdicionarEvento(new CursoCadastradoEvent(curso.Id, cursoViewModel.Titulo!, cursoViewModel.Valor, cursoViewModel.Descricao!, cursoViewModel.CargaHoraria));

            return await _cursoRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> AdicionarAula(AulaViewModel aulaViewModel)
        {

            if (!await ModelValido(aulaViewModel)) return false;

            var aula = new Aula(aulaViewModel.Titulo!, aulaViewModel.Duracao, aulaViewModel.Conteudo!);

            var curso = await CursoExistente(aulaViewModel.CursoId);
            if (curso == null) return false;

            if (await AulaComMesmoTitulo(curso, aula)) return false;
            if (await AulaExcedeCargaHorariaCurso(curso, aula)) return false;

            curso.AdicionarAula(aula);

            _cursoRepository.Adicionar(aula);

            aula.AdicionarEvento(new AulaAdicionadaEvent(aulaViewModel.CursoId, aulaViewModel.Titulo!, aulaViewModel.Duracao, aulaViewModel.Conteudo!));

            return await _cursoRepository.UnitOfWork.CommitAsync();
        }

        private async Task<bool> AulaExcedeCargaHorariaCurso(Curso curso, Aula aula)
        {
            if (!curso.AulaExcedeCargaHoraria(aula.Duracao)) return false;
            await _mediatorHandler.PublicarNotificacao(new DomainNotification(nameof(CursoApplicationService.AdicionarAula), Curso.AdicionarAulaUltrapassaCargaHoraria));
            return true;
        }

        private async Task<bool> AulaComMesmoTitulo(Curso curso, Aula aula)
        {
            if (!curso.ExisteAulaComMesmoTitulo(aula.Titulo)) return false;
            await _mediatorHandler.PublicarNotificacao(new DomainNotification(nameof(CursoApplicationService.AdicionarAula), Curso.AulaJaExiste + $" '{aula.Titulo}'."));
            return true;
        }

        private async Task<Curso?> CursoExistente(Guid cursoId)
        {
            var curso = await _cursoRepository.ObterPorIdAsync(cursoId);
            if (curso == null)
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(this.GetType().Name, CursoNaoEncontrado));
            return curso;
        }

        private async Task<bool> ModelValido(object viewModel)
        {
            var validationResults = ViewModelTestsHelpers.ValidateModel(viewModel);

            if (!validationResults.Any()) return true;

            foreach (var error in validationResults)
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification(error.MemberNames.First(), error.ErrorMessage!));
            }
            return false;
        }
    }
}
