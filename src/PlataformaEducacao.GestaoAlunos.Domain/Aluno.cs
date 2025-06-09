using PlataformaEducacao.Core.DomainObjects;

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
            Validar();
        }

        public string? Nome { get; private set; }
        public IReadOnlyCollection<Matricula> Matriculas => _matriculas;

        public void Matricular(Matricula matricula)
        {
            if (JaMatriculado(matricula.CursoId))
            {
                throw new DomainException("Aluno já está matriculado neste curso.");
            }
            matricula.VincularAluno(this.Id);
            _matriculas.Add(matricula);
        }

        public bool JaMatriculado(Guid cursoId)
        {
            return _matriculas.Any(m => m.CursoId == cursoId);
        }

        public void PagarMatricula(Guid matriculaId)
        {
            var matricula = Matriculas.FirstOrDefault(m => m.Id == matriculaId && m.StatusMatricula == StatusMatricula.PENDENTE_PAGAMENTO);
            if (matricula is null) throw new DomainException("Matricula não encontrada ou não está pendende de pagamento.");
            matricula.Pagar();
        }

        public void FinalizarAula(Guid aulaId, Guid cursoId)
        {
            var matricula = Matriculas.FirstOrDefault(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatricula.EM_ANDAMENTO);
            if (matricula is null) throw new DomainException("Matricula não encontrada ou não está em andamento.");
            matricula.FinalizarAula(aulaId);
        }

        public void AtualizarProgresso(Guid cursoId, int totalAulas)
        {
            var matricula = Matriculas.FirstOrDefault(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatricula.EM_ANDAMENTO);
            if (matricula is null) throw new DomainException("Matricula não encontrada ou não está em andamento.");

            matricula.AtualizarProgresso(totalAulas);
        }

        public void Validar()
        {            
            Validacoes.ValidarSeDiferente(Id, Guid.Empty, "O Id do(a) aluno(a) não pode ser vazio");
            Validacoes.ValidarSeVazio(Nome, "O nome do(a) aluno(a) não pode ser vazio");            
        }

        internal void FinalizarCurso(Guid cursoId)
        {
            var matricula = Matriculas.FirstOrDefault(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatricula.EM_ANDAMENTO);
            if (matricula is null) throw new DomainException("Matricula não encontrada ou não está em andamento.");
            matricula.Finalizar();
        }

        internal void GerarCertificado(Guid cursoId)
        {
            var matricula = Matriculas.FirstOrDefault(m => m.CursoId == cursoId && m.StatusMatricula == StatusMatricula.CONCLUIDA);
            if (matricula is null) throw new DomainException("Matricula não encontrada ou curso não finalizado.");
            matricula.GerarCertificado();
        }

        





        //public Matricula MatricularEmCurso(string codigoCurso, string nomeCurso)
        //{
        //    if (!Ativo)
        //        throw new InvalidOperationException("Aluno inativo não pode se matricular em cursos");

        //    if (_matriculas.Any(m => m.CodigoCurso == codigoCurso && m.Status == StatusMatricula.Ativa))
        //        throw new InvalidOperationException("Aluno já possui matrícula ativa neste curso");

        //    var matricula = new Matricula(codigoCurso, nomeCurso);
        //    _matriculas.Add(matricula);

        //    return matricula;
        //}

        //public void AtualizarProgressoCurso(Guid matriculaId, decimal percentualConclusao, int horasEstudadas, decimal notaFinal = 0)
        //{
        //    var matricula = _matriculas.FirstOrDefault(m => m.Id == matriculaId);
        //    if (matricula == null)
        //        throw new ArgumentException("Matrícula não encontrada");

        //    matricula.AtualizarProgresso(percentualConclusao, horasEstudadas, notaFinal);
        //}

        //public void GerarCertificado(Guid matriculaId, string numeroCertificado, int cargaHoraria)
        //{
        //    var matricula = _matriculas.FirstOrDefault(m => m.Id == matriculaId);
        //    if (matricula == null)
        //        throw new ArgumentException("Matrícula não encontrada");

        //    matricula.GerarCertificado(numeroCertificado, cargaHoraria);
        //}

        //public void CancelarMatricula(Guid matriculaId)
        //{
        //    var matricula = _matriculas.FirstOrDefault(m => m.Id == matriculaId);
        //    if (matricula == null)
        //        throw new ArgumentException("Matrícula não encontrada");

        //    matricula.Cancelar();
        //}

        //public void AtualizarDados(string nome, string email)
        //{
        //    if (string.IsNullOrWhiteSpace(nome))
        //        throw new ArgumentException("Nome é obrigatório");

        //    if (string.IsNullOrWhiteSpace(email))
        //        throw new ArgumentException("Email é obrigatório");

        //    Nome = nome;
        //    Email = email;
        //}

        //public void Inativar()
        //{
        //    Ativo = false;
        //    // Cancelar todas as matrículas ativas
        //    foreach (var matricula in _matriculas.Where(m => m.Status == StatusMatricula.Ativa))
        //    {
        //        matricula.Cancelar();
        //    }
        //}

        //public void Reativar()
        //{
        //    Ativo = true;
        //}

        //public IEnumerable<Matricula> ObterMatriculasAtivas()
        //{
        //    return _matriculas.Where(m => m.Status == StatusMatricula.Ativa);
        //}

        //public IEnumerable<Certificado> ObterCertificados()
        //{
        //    return _matriculas
        //        .Where(m => m.Certificado != null)
        //        .Select(m => m.Certificado);
        //}
    }
}
