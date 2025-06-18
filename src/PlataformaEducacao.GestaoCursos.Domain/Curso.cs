using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.GestaoCursos.Domain.Validations;

namespace PlataformaEducacao.GestaoCursos.Domain
{
    public class Curso : Entity, IAggregateRoot
    {
        private readonly List<Aula> _aulas;
        public string Nome { get; private set; }
        public decimal Valor { get; private set; }
        public ConteudoProgramatico ConteudoProgramatico { get; private set; }
        public IReadOnlyCollection<Aula>? Aulas => _aulas;

        private Curso()
        {
            _aulas = new List<Aula>();
        }

        public Curso(string nome, decimal valor, ConteudoProgramatico conteudoProgramatico)
            : this()
        {
            this.Nome = nome;
            this.ConteudoProgramatico = conteudoProgramatico;
            this.Valor = valor;
            
        }

        public void AdicionarAula(Aula aula)
        {
            if (Aulas?.Any(a => a.Titulo == aula.Titulo) ?? false)
                throw new DomainException("Já existe uma aula com este título no curso");
            aula.AssociarCurso(this.Id);
            _aulas.Add(aula);
        }

        public override bool EhValido()
        {
            ValidationResult = new CursoValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
