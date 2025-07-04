using FluentValidation.Results;
using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Gestao.Domain.Validations;


namespace PlataformaEducacao.Gestao.Domain
{
    public class Aluno : Entity, IAggregateRoot
    {        
        public const int NOME_MIN_CARACTERES = 2;
        public const int NOME_MAX_CARACTERES = 150;
        public const string AlunoJaMatriculado = "Aluno já matriculado neste curso.";
        public const string AlunoNaoEstaMatriculado = "Aluno não está matriculado neste curso.";



        public string Nome { get; private set; }
        public string Email { get; private set; }
        private readonly List<Matricula> _matriculas;
        public IReadOnlyCollection<Matricula> Matriculas => _matriculas;
        public Aluno(string nome, string email)
        {
            Nome = nome;
            Email = email;
            _matriculas = new List<Matricula>();
        }

        public void Matricular(Matricula matricula)
        {
            if (EstaMatriculado(matricula.Curso.CursoId))
                throw new DomainException(AlunoJaMatriculado);
            _matriculas.Add(matricula);
        }

        public void PagarMatricula(Guid cursoId)
        {   
            Matricula matricula = ObterMatricula(cursoId);            
            matricula.Pagar();            
        }

        public bool EstaMatriculado(Guid cursoId)
        {
            return _matriculas.Any(m => m.Curso.CursoId == cursoId);
        }

        public Matricula ObterMatricula(Guid cursoId)
        {
            if (!EstaMatriculado(cursoId)) throw new DomainException(AlunoNaoEstaMatriculado);
            return _matriculas.First(m => m.Curso.CursoId == cursoId);            
        }

        public override bool EhValido()
        {
            ValidationResult = new AlunoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

       
    }
}
