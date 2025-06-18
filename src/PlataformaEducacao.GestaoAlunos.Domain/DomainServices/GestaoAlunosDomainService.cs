using MediatR;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Core.Messages.Messages.IntegrationEvents;
using PlataformaEducacao.GestaoAlunos.Domain.Events;
using PlataformaEducacao.GestaoAlunos.Domain.Interfaces;

namespace PlataformaEducacao.GestaoAlunos.Domain.DomainServices
{
    public class GestaoAlunosDomainService : DomainService, IGestaoAlunosDomainService
    {
        private readonly IAlunoRepository _alunoRepository;


        public GestaoAlunosDomainService(IAlunoRepository alunoRepository, IMediatorHandler mediator)
            : base(mediator)
        {
            _alunoRepository = alunoRepository;
        }
        public async Task MatricularAluno(Guid alunoid, Guid cursoId, string nomeCurso)
        {
            var aluno = await _alunoRepository.ObterAlunoParaMatriculaPorId(alunoid);
            if (aluno is null) throw new DomainException("Aluno não encontrado.");

            aluno.Matricular(new Matricula(alunoid, cursoId, nomeCurso));
            var matricula = aluno.Matriculas.First(x => x.CursoId == cursoId);
            if(!ValidarEntidade(matricula)) return;
            aluno.AdicionarEvento(new AlunoMatriculadoEvent(alunoid, cursoId));
            _alunoRepository.Adicionar(matricula);
            await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task PagarMatricula(Guid alunoId, Guid matriculaId)
        {
            var aluno = await _alunoRepository.ObterAlunoPorId(alunoId);
            if (aluno is null) throw new DomainException("Aluno não encontrado.");            
            aluno.PagarMatricula(matriculaId);
            var matricula = aluno.Matriculas.First(m => m.Id == matriculaId);
            if(!ValidarEntidade(matricula)) return;
            matricula.AdicionarEvento(new MatriculaPagaEvent(alunoId, matriculaId));
            _alunoRepository.Atualizar(matricula);
            await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task FinalizarAula(Guid alunoId, Guid cursoId, Guid aulaId, int totalAulasCurso)
        {
            var aluno = await _alunoRepository.ObterAlunoPorId(alunoId);
            if (aluno is null) throw new DomainException("Aluno não encontrado.");            
            aluno.FinalizarAula(aulaId, cursoId);
            var aulaFinalizada = aluno.Matriculas
                .First(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatriculaEnum.EM_ANDAMENTO)
                .AulasFinalizadas.First(a => a.AulaId == aulaId);
            if(!ValidarEntidade(aulaFinalizada)) return;
            aulaFinalizada.AdicionarEvento(new AulaFinalizadaEvent(alunoId, cursoId, aulaId, totalAulasCurso));
            _alunoRepository.Adicionar(aulaFinalizada);
            await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task AtualizarProgresso(Guid alunoId, Guid cursoId, Guid aulaId, int totalAulas)
        {
            var aluno = await _alunoRepository.ObterAlunoPorId(alunoId);
            if (aluno is null) throw new DomainException("Aluno não encontrado.");            

            aluno.AtualizarProgresso(cursoId, totalAulas);
            var matricula = aluno.Matriculas.First(m => m.CursoId == cursoId);
            if(!ValidarEntidade(matricula)) return;
            matricula.AdicionarEvento(new ProgressoAtualizadoEvent(alunoId, cursoId));
            _alunoRepository.Atualizar(matricula);
            await _alunoRepository.UnitOfWork.CommitAsync();
        }

        public async Task FinalizarCurso(Guid alunoId, Guid cursoId)
        {
            var aluno = await _alunoRepository.ObterAlunoPorId(alunoId);
            if (aluno is null) throw new DomainException("Aluno não encontrado.");
            
            aluno.FinalizarCurso(cursoId);

            var matricula = aluno.Matriculas.First(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatriculaEnum.CONCLUIDA);
            if (matricula is not null)
            {
                if(!ValidarEntidade(matricula)) return;
                matricula.AdicionarEvento(new CursoFinalizadoEvent(alunoId, cursoId));
                _alunoRepository.Atualizar(matricula);
                await _alunoRepository.UnitOfWork.CommitAsync();
            }
        }

        public async Task GerarCertificado(Guid alunoId, Guid cursoId)
        {
            var aluno = await _alunoRepository.ObterAlunoPorId(alunoId);
            if (aluno is null) throw new DomainException("Aluno não encontrado.");
            
            aluno.GerarCertificado(cursoId);
            var certificado = aluno.Matriculas.First(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatriculaEnum.CONCLUIDA).Certificado;            
            if (certificado is not null)
            {
                if(!ValidarEntidade(certificado)) return;
                certificado.AdicionarEvento(new CertificadoGeradoEvent(alunoId, cursoId, certificado.Id));
                _alunoRepository.Adicionar(certificado);
                await _alunoRepository.UnitOfWork.CommitAsync();
            }
        }

        public async Task CadastrarAluno(Guid id, string nome)
        {
            var aluno = new Aluno(id, nome);
            var alunoCadastrado = await _alunoRepository.ObterAlunoPorId(id);
            if (alunoCadastrado is not null) throw new DomainException("Aluno já cadastrado com este Id.");
            if(!ValidarEntidade(aluno)) return;
            aluno.AdicionarEvento(new AlunoCadastradoEvent(id, nome));
            _alunoRepository.Adicionar(aluno);
            await _alunoRepository.UnitOfWork.CommitAsync();
        }
    }
}
