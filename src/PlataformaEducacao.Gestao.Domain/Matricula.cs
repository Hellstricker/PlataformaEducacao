using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Gestao.Domain.Validations;

namespace PlataformaEducacao.Gestao.Domain
{
    public class Matricula : Entity
    {
        public const int MIN_NOMECURSO_CHAR = 10;
        public const int MAX_NOMECURSO_CHAR = 150;
        public const string MatriculaNaoEstaPendente = "Matricula não está pendente de pagamento.";

        public Guid AlunoId { get; private set; }
        public CursoMatriculado Curso { get; private set; }
        
        public StatusMatriculaEnum Status { get; private set; }

        // EF Relation
        public Aluno? Aluno { get; private set; }
        

        public Matricula() : base() { Status = StatusMatriculaEnum.PENDENTE_PAGAMENTO; }

        public Matricula(Guid alunoId, Guid cursoId, string? cursoNome, decimal cursoValor, int cursoTotalAulas) : this()
        {
            AlunoId = alunoId;
            Curso = new CursoMatriculado(cursoId, cursoNome, cursoValor, cursoTotalAulas);            
        }

        public override bool EhValido()
        {
            ValidationResult = new MatriculaValidation().Validate(this);
            return ValidationResult.IsValid;
        }

        public void Pagar()
        {
            if (!PodeSerPaga()) throw new DomainException(MatriculaNaoEstaPendente);
            Status = StatusMatriculaEnum.EM_ANDAMENTO;
        }

        public void TornarPendenteDePagamento()
        {
            Status = StatusMatriculaEnum.PENDENTE_PAGAMENTO;
        }

        public bool PodeSerPaga()
        {
            return Status == StatusMatriculaEnum.PENDENTE_PAGAMENTO;
        }

        public static class MariculaFactory
        {
            public static Matricula NovaMatriculaCorreta(Guid alunoId, Guid cursoId, string? cursoNome, decimal cursoValor, int cursoTotalAulas)
            {
                var matricula = new Matricula
                {
                    AlunoId = alunoId,
                    Curso = new CursoMatriculado(cursoId, cursoNome, cursoValor, cursoTotalAulas)
                };

                matricula.TornarPendenteDePagamento();

                return matricula;
            }

            public static Matricula NovaMatriculaInCorreta(Guid alunoId, Guid cursoId, string? cursoNome, decimal cursoValor, int cursoTotalAulas)
            {
                var matricula = new Matricula
                {
                    AlunoId = alunoId,
                    Curso = new CursoMatriculado(cursoId, cursoNome, cursoValor, cursoTotalAulas)
                };

                matricula.TornarPendenteDePagamento();
                matricula.Pagar();

                return matricula;
            }
        }

        
    }

    
    public class CursoMatriculado
    {
        public Guid CursoId { get; private set; }
        public string? CursoNome { get; private set; }
        public decimal CursoValor { get; private set; }
        public int CursoTotalAulas { get; private set; }

        public CursoMatriculado(Guid cursoId, string? cursoNome, decimal cursoValor, int cursoTotalAulas)
        {
            CursoId = cursoId;
            CursoNome = cursoNome;
            CursoValor = cursoValor;
            CursoTotalAulas = cursoTotalAulas;
        }
    }


}
