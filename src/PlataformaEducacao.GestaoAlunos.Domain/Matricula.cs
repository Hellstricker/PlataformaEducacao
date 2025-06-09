using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.GestaoAlunos.Domain
{
    public class Matricula : Entity
    {
        private readonly List<AulaFinalizada> _aulasFinalizadas;
        private Matricula()
        {
            StatusMatricula = StatusMatricula.PENDENTE_PAGAMENTO;
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
        public StatusMatricula StatusMatricula { get; private set; }
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
            StatusMatricula = StatusMatricula.EM_ANDAMENTO;
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
                StatusMatricula = StatusMatricula.CONCLUIDA;
            }
        }

        public void GerarCertificado()
        {
            if(StatusMatricula != StatusMatricula.CONCLUIDA)
                throw new DomainException("Só é possível gerar certificado para matrículas concluídas");
            Certificado = new Certificado(NomeCurso, Id);                
        }




        //public void AtualizarProgresso(decimal percentualConclusao, int horasEstudadas, decimal notaFinal = 0)
        //{
        //    if (Status != StatusMatricula.Ativa)
        //        throw new InvalidOperationException("Não é possível atualizar progresso de matrícula inativa");

        //    var dataConclusao = percentualConclusao >= 100 ? DateTime.UtcNow : (DateTime?)null;

        //    HistoricoAprendizado = new HistoricoAprendizado(
        //        HistoricoAprendizado.DataInicio,
        //        percentualConclusao,
        //        horasEstudadas,
        //        notaFinal,
        //        dataConclusao
        //    );

        //    if (percentualConclusao >= 100)
        //    {
        //        Status = StatusMatricula.Concluida;
        //        DataConclusao = DateTime.UtcNow;
        //    }
        //}

        //public void GerarCertificado(string numeroCertificado, int cargaHoraria)
        //{
        //    if (Status != StatusMatricula.Concluida)
        //        throw new InvalidOperationException("Só é possível gerar certificado para matrículas concluídas");

        //    if (Certificado != null)
        //        throw new InvalidOperationException("Certificado já foi gerado para esta matrícula");

        //    Certificado = new Certificado(numeroCertificado, NomeCurso, cargaHoraria, HistoricoAprendizado.NotaFinal);
        //}

        //public void Cancelar()
        //{
        //    if (Status == StatusMatricula.Cancelada)
        //        throw new InvalidOperationException("Matrícula já está cancelada");

        //    Status = StatusMatricula.Cancelada;
        //}

        //public void Reativar()
        //{
        //    if (Status != StatusMatricula.Cancelada)
        //        throw new InvalidOperationException("Só é possível reativar matrículas canceladas");

        //    Status = StatusMatricula.Ativa;
        //}

    }
}