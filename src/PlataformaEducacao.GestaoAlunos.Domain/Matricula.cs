using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.GestaoAlunos.Domain.Validations;

namespace PlataformaEducacao.GestaoAlunos.Domain
{
    public class Matricula : Entity
    {
        private readonly List<AulaFinalizada> _aulasFinalizadas;
        private Matricula()
        {
            StatusMatricula = StatusMatriculaEnum.PENDENTE_PAGAMENTO;
            _aulasFinalizadas = new List<AulaFinalizada>();
        }

        public Matricula(Guid alunoId, Guid cursoId, string nomeCurso)
            : this()
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            NomeCurso = nomeCurso;
        }

        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public string NomeCurso { get;private set; }
        public StatusMatriculaEnum StatusMatricula { get; private set; }
        public IReadOnlyCollection<AulaFinalizada> AulasFinalizadas => _aulasFinalizadas;

        public HistoricoAprendizado HistoricoAprendizado { get; private set; }
        public Certificado Certificado { get; private set; }

        public Aluno Aluno { get; private set; }

        public void VincularAluno(Guid alunoId)
        {
            AlunoId = alunoId;
        }

        public void Pagar()
        {
            StatusMatricula = StatusMatriculaEnum.EM_ANDAMENTO;
        }

        public void FinalizarAula(Guid aulaId)
        {
            if(_aulasFinalizadas.Any(a=>a.AulaId == aulaId))
                throw new DomainException("Aula já finalizada.");
            _aulasFinalizadas.Add(new AulaFinalizada(this.Id, aulaId));
        }

        public void AtualizarProgresso(int totalAulas)
        {
            HistoricoAprendizado.AtualizarProgresso(_aulasFinalizadas.Count, totalAulas);
        }

        public void Finalizar()
        {
            if (HistoricoAprendizado.Progresso == 100)
            {
                StatusMatricula = StatusMatriculaEnum.CONCLUIDA;
            }
        }

        public void GerarCertificado()
        {
            if(StatusMatricula != StatusMatriculaEnum.CONCLUIDA)
                throw new DomainException("Só é possível gerar certificado para matrículas concluídas");
            Certificado = new Certificado(NomeCurso, Id);                
        }

        public override bool EhValido()
        {
            ValidationResult = new MatriculaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}