using PlataformaEducacao.Core.DomainObjects;

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

            Validar();
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

        public void Validar()
        {
            Validacoes.ValidarSeVazio(Titulo, "O título da aula não pode ser vazio");
            Validacoes.ValidarSeVazio(Conteudo, "O conteúdo da aula não pode ser vazio");
            Validacoes.ValidarSeDiferente(CursoId, Guid.Empty, "O curso precisa ser informado");
        }
    }
}
