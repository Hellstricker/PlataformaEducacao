using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Gestao.Domain.Validations;

namespace PlataformaEducacao.Gestao.Domain
{
    public class Matricula : Entity
    {
        public const int MIN_NOMECURSO_CHAR = 10;
        public const int MAX_NOMECURSO_CHAR = 150;
        public const string MatriculaNaoEstaPendente = "Matricula não está pendente de pagamento.";
        public const string MatriculaNaoEstaEmAndamento = "Matricula não está em andamento";

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

        internal void Pagar()
        {
            if (!PodeSerPaga()) throw new DomainException(MatriculaNaoEstaPendente);
            Status = StatusMatriculaEnum.EM_ANDAMENTO;
        }

        internal void TornarPendenteDePagamento()
        {
            Status = StatusMatriculaEnum.PENDENTE_PAGAMENTO;
        }

        public bool PodeSerPaga()
        {
            return Status == StatusMatriculaEnum.PENDENTE_PAGAMENTO;
        }

        public bool PodeFinalizarAula()
        {
            return Status == StatusMatriculaEnum.EM_ANDAMENTO;
        }

        internal void FinalizarAula(Guid aulaId)
        {
            if (!PodeFinalizarAula())
                throw new DomainException(Matricula.MatriculaNaoEstaEmAndamento);
            Curso.FinalizarAula(Id, aulaId);
        }

        internal bool PodeFinalizar()
        {
            return
                Status != StatusMatriculaEnum.PENDENTE_PAGAMENTO &&
                Curso.HistoricoAprendizado != null &&
                Curso.HistoricoAprendizado.Progresso == 100;                
        }

        internal void Finalizar()
        {
            if(PodeFinalizar())
                Status = StatusMatriculaEnum.CONCLUIDA;
        }

        internal bool EstaFinalizada()
        {
            return Status == StatusMatriculaEnum.CONCLUIDA;
        }

        internal bool NaoEstaFinalizada()
        {
            return !EstaFinalizada();
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
        public HistoricoAprendizado? HistoricoAprendizado { get; set; }

        public CursoMatriculado(Guid cursoId, string? cursoNome, decimal cursoValor, int cursoTotalAulas)
        {
            CursoId = cursoId;
            CursoNome = cursoNome;
            CursoValor = cursoValor;
            CursoTotalAulas = cursoTotalAulas;
        }

        internal void FinalizarAula(Guid matriculaId, Guid aulaFinalizadaId)
        {
            if(HistoricoAprendizado == null)
                HistoricoAprendizado = new HistoricoAprendizado();

            HistoricoAprendizado.AtualizarProgresso(matriculaId, aulaFinalizadaId, CursoTotalAulas);
        }
    }

    public class HistoricoAprendizado
    {
        
        public decimal? Progresso { get; private set; }
        private readonly List<AulaFinalizada> _aulasFinalizadas = new List<AulaFinalizada>();
        public IEnumerable<AulaFinalizada> AulasFinalizadas => _aulasFinalizadas;

        internal bool EstaCompleto()
        {
            return Progresso == 100M;
        }

        internal void AtualizarProgresso(Guid mastriculaId, Guid aulaId, int totalAulas)
        {
            if (totalAulas <= 0) throw new DomainException("Total de aulas deve ser maior que zero.");            
            if(_aulasFinalizadas.Any(x=>x.AulaId == aulaId)) return;
            _aulasFinalizadas.Add(new AulaFinalizada(mastriculaId, aulaId));
            Progresso = (decimal)_aulasFinalizadas.Count() / totalAulas * 100;
        }
    }
}
