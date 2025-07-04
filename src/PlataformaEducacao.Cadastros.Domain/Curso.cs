using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Core.Validations;

namespace PlataformaEducacao.Cadastros.Domain
{
    public class Curso : Entity, IAggregateRoot
    {
        public const int MINIMO_CARACTERES_TITULO = 10;
        public const int MAXIMO_CARACTERES_TITULO = 150;        
        

        public const string TituloEmBrancoOuNulo  = "O título do curso não pode ser em branco ou nulo.";
        public const string ValorMenorOuIgualAZero = "O valor não pode ser menor ou igual a zero.";
        public const string ConteudoProgramaticoNulo = "O conteúdo programático não pode ser nulo.";        
        public const string TituloSemMinimoCaracteres = "O título do curso deve ter no mínimo 10 caracteres.";
        public const string TituloMaiorMaximoCaracteres = "O título do curso deve ter no máximo 150 caracteres.";
        public const string AulaJaExiste = "Já existe uma aula com o título informado.";
        public const string AdicionarAulaUltrapassaCargaHoraria = "A adição desta aula ultrapassa a carga horária do curso.";


        public string Titulo { get; private set; }
        public decimal Valor { get; private set; }
        public ConteudoProgramatico ConteudoProgramatico { get; private set; }        
        
        private readonly List<Aula> _aulas = new List<Aula>();
        public IReadOnlyCollection<Aula> Aulas => _aulas;
        
        public Curso() { } // Construtor vazio para EF Core

        public Curso(string titulo, decimal valor, ConteudoProgramatico conteudoProgramatico)            
        {
            Titulo = titulo;
            Valor = valor;
            ConteudoProgramatico = conteudoProgramatico;

            Validar();
        }

        public void AdicionarAula(Aula aula)
        {
            aula.AssociarCurso(Id);
            ValidarAulaComTituloExistente(aula.Titulo);
            ValidarCargaHorariaExcedida(aula.Duracao);
            _aulas.Add(aula);
        }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(Titulo, TituloEmBrancoOuNulo);
            Validacoes.ValidarSeStringEntreCaracteres(Titulo, MINIMO_CARACTERES_TITULO, MAXIMO_CARACTERES_TITULO, TituloSemMinimoCaracteres, TituloMaiorMaximoCaracteres);
            Validacoes.ValidarSeMenorOuIgual(Valor, 0, ValorMenorOuIgualAZero);
            Validacoes.ValidarSeNulo(ConteudoProgramatico, ConteudoProgramaticoNulo);
            Validacoes.ValidarSeVazio(ConteudoProgramatico.Descricao, ConteudoProgramatico.DescricaoEmBrancoOuNulo);
            Validacoes.ValidarSeStringEntreCaracteres(ConteudoProgramatico.Descricao, ConteudoProgramatico.MINIMO_CARACTERES_DESCRICAO, ConteudoProgramatico.MAXIMO_CARACTERES_DESCRICAO, ConteudoProgramatico.DescricaoSemMinimoCaracteres, ConteudoProgramatico.DescricaoMaiorMaximoCaracteres);
            Validacoes.ValidarSeMenorOuIgual(ConteudoProgramatico.CargaHoraria, 0, ConteudoProgramatico.CargaHorariaMenorOuIgualAZero);            
        }

        private void ValidarAulaComTituloExistente(string tituloAula)
        {
            if (ExisteAulaComMesmoTitulo(tituloAula))
            {
                throw new DomainException($"{AulaJaExiste} '{tituloAula}'.");
            }
        }

        public bool ExisteAulaComMesmoTitulo(string tituloAula)
        {
            return _aulas.Any(a => a.Titulo == tituloAula);
        }

        private void ValidarCargaHorariaExcedida(int duracao)
        {
            if (AulaExcedeCargaHoraria(duracao))
            {
                throw new DomainException($"{AdicionarAulaUltrapassaCargaHoraria} Carga Horária {ConteudoProgramatico.CargaHoraria} minuto(s)");
            }
        }

        public bool AulaExcedeCargaHoraria(int duracao)
        {
            return _aulas.Sum(a => a.Duracao) + duracao > ConteudoProgramatico.CargaHoraria;
        }
    }
}
