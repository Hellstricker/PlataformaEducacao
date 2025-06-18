using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.GestaoCursos.Domain.Validations;

namespace PlataformaEducacao.GestaoCursos.Domain
{
    public class Aula : Entity
    {
        private Aula() : base() { }

        public Aula(string? titulo, string? conteudo, int totalMinutos, Guid cursoId) : this()
        {
            Titulo = titulo;
            Conteudo = conteudo;
            TotalMinutos = totalMinutos;
            CursoId = cursoId;
        }

        public string? Titulo { get; private set; }
        public string? Conteudo { get; private set; }
        public int TotalMinutos { get; private set; }
        public Guid CursoId { get; private set; }
        public virtual Curso? Curso { get; private set; }

        public void AssociarCurso(Guid id)
        {
            this.CursoId = id;
        }

        public override bool EhValido()
        {
            ValidationResult = new AulaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
