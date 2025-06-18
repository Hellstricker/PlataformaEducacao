using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.GestaoAlunos.Domain.Validations;

namespace PlataformaEducacao.GestaoAlunos.Domain
{
    public class Aluno : Entity, IAggregateRoot
    {
        private readonly List<Matricula> _matriculas;

        protected Aluno(Guid Id) 
            : base(Id)
        {
            _matriculas = new List<Matricula>();
        }

        public Aluno(Guid id, string? nome) 
            : this(id)
        {            
            Nome = nome;            
        }

        public string? Nome { get; private set; }
        public IReadOnlyCollection<Matricula> Matriculas => _matriculas;

        public void Matricular(Matricula matricula)
        {
            if (EstaMatriculado(matricula.CursoId))
                throw new DomainException("Aluno já está matriculado neste curso.");
            
            matricula.VincularAluno(this.Id);
            _matriculas.Add(matricula);
        }

        public bool EstaMatriculado(Guid cursoId)
        {
            return _matriculas.Any(m => m.CursoId == cursoId);
        }

        public void PagarMatricula(Guid matriculaId)
        {
            var matricula = Matriculas.FirstOrDefault(m => m.Id == matriculaId && m.StatusMatricula == StatusMatriculaEnum.PENDENTE_PAGAMENTO);
            if (matricula is null) throw new DomainException("Matricula não encontrada ou não está pendende de pagamento.");
            matricula.Pagar();
        }

        public void FinalizarAula(Guid aulaId, Guid cursoId)
        {
            var matricula = Matriculas.FirstOrDefault(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatriculaEnum.EM_ANDAMENTO);
            if (matricula is null) throw new DomainException("Matricula não encontrada ou não está em andamento.");
            matricula.FinalizarAula(aulaId);
        }

        public void AtualizarProgresso(Guid cursoId, int totalAulas)
        {
            var matricula = Matriculas.FirstOrDefault(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatriculaEnum.EM_ANDAMENTO);
            if (matricula is null) throw new DomainException("Matricula não encontrada ou não está em andamento.");

            matricula.AtualizarProgresso(totalAulas);
        }

        internal void FinalizarCurso(Guid cursoId)
        {            
            var matricula = Matriculas.FirstOrDefault(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatriculaEnum.EM_ANDAMENTO);
            if (matricula is null) throw new DomainException("Matricula não encontrada ou não está em andamento.");
            matricula.Finalizar();
        }

        internal void GerarCertificado(Guid cursoId)
        {            
            var matricula = Matriculas.FirstOrDefault(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatriculaEnum.CONCLUIDA);
            if (matricula is null) throw new DomainException("Matricula não encontrada ou curso não finalizado.");
            matricula.GerarCertificado();
        }

        public override bool EhValido()
        {
            ValidationResult = new AlunoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
