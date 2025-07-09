using FluentValidation.Results;
using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Gestao.Domain.Validations;
using System.Reflection;


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

        private readonly List<Certificado> _certificados;
        public IReadOnlyCollection<Certificado> Certificados => _certificados;

        public Aluno(string nome, string email)
        {
            Nome = nome;
            Email = email;
            _matriculas = new List<Matricula>();
            _certificados = new List<Certificado>();
        }

        public void Matricular(Matricula matricula)
        {
            if (EstaMatriculado(matricula.Curso.CursoId)) throw new DomainException(AlunoJaMatriculado);
            _matriculas.Add(matricula);
        }

        public void PagarMatricula(Guid cursoId)
        {   
            var matricula = ObterMatricula(cursoId);            
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

        public void FinalizarAula(Guid cursoId, Guid aulaId)
        {
            var matricula = ObterMatricula(cursoId);
            matricula.FinalizarAula(aulaId);
        }

        public override bool EhValido()
        {
            ValidationResult = new AlunoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public bool PodeFinalizarMatricula(Guid cursoId)
        {
            var matricula = ObterMatricula(cursoId);
            return matricula.PodeFinalizar();
        }

        public void FinalizarMatricula(Guid cursoId)
        {
            var matricula = ObterMatricula(cursoId);
            matricula.Finalizar();
        }

        public Matricula? ObterMatriculaParaFinalizar(Guid cursoId)
        {
            var matricula = ObterMatricula(cursoId);
            if (!matricula.PodeFinalizar()) return null;
            return matricula;
        }

        public void GerarCertificado(Guid cursoId)
        {
            var matricula = ObterMatricula(cursoId);

            if(matricula.NaoEstaFinalizada()) return;
           
            _certificados.Add(new Certificado(Id, matricula.Curso.CursoNome!));
        }

        public Matricula? ObterMatriculaCursoFinalizado(Guid cursoId)
        {
            var matricula = ObterMatricula(cursoId);

            if (matricula.NaoEstaFinalizada()) return null;
            return matricula;
        }

        public void CancelarPagamentoMatricula(Guid cursoId)
        {
            var matricula = ObterMatricula(cursoId);
            matricula.CancelarPagamento();
        }
    }

    public class Certificado : Entity
    {
        public Guid AlunoId { get; set; }
        public Guid NumeroCertificado { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string NomeCurso { get; private set; }
        public bool Ativo { get; private set; }

        public Aluno Aluno { get; private set; }

        public Certificado(Guid alunoId, string nomeCurso)
        {
            AlunoId = alunoId;
            NumeroCertificado = Guid.NewGuid();
            NomeCurso = nomeCurso;
            Ativo = true;            
        }

        public override bool EhValido()
        {
            ValidationResult = new CertificadoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
