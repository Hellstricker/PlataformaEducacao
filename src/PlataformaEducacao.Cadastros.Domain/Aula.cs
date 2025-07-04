using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Core.Validations;

namespace PlataformaEducacao.Cadastros.Domain
{
    public class Aula : Entity
    {
        public const int MINIMO_CARACTERES_TITULO = 10;
        public const int MAXIMO_CARACTERES_TITULO = 150;
        public const int MINIMO_DURACAO = 0; // em minutos
        public const int MAXIMO_CARACTERES_CONTEUDO = 500;
        public const int MINIMO_CARACTERES_CONTEUDO = 15;


        public const string TituloEmBrancoOuNulo = "O título da aula não pode ser em branco ou nulo.";
        public const string TituloSemMinimoDeCaracteres = "O título da aula deve ter no mínimo 10 caracteres.";
        public const string TituloMaiorMaximoCaracteres = "O título da aula deve ter no máximo 150 caracteres.";
        public const string DuracaoDeveSerMaiorQueMinimo = "A duração da aula deve ser maior que zero minutos.";
        public const string ConteudoEmBrancoOuNulo = "O conteúdo da aula não pode ser em branco ou nulo.";
        public const string ConteudoUltrapassouMaximoCaracteres = "O conteúdo da aula deve ter no máximo 500 caracteres.";
        public const string ConteudoSemMinimoDeCaracteres = "O conteúdo da aula deve ter no mínimo 15 caracteres.";

        public string Titulo { get; private set; }
        public int Duracao { get; private set; }
        public string Conteudo { get; private set; }
        public Guid CursoId { get; private set; }

        public Curso Curso { get; private set; } // Navegação para o curso relacionado

        public Aula() { } // Construtor vazio para EF Core

        public Aula(string titulo, int duracao, string conteudo)
        {
            Titulo = titulo;
            Duracao = duracao;
            Conteudo = conteudo;

            Validar();
        }

        internal void AssociarCurso(Guid cursoId)
        {
            CursoId = cursoId;
        }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(Titulo, TituloEmBrancoOuNulo);
            Validacoes.ValidarSeStringEntreCaracteres(Titulo, MINIMO_CARACTERES_TITULO, MAXIMO_CARACTERES_TITULO, TituloSemMinimoDeCaracteres, TituloMaiorMaximoCaracteres);
            Validacoes.ValidarSeMenorOuIgual(Duracao, MINIMO_DURACAO, DuracaoDeveSerMaiorQueMinimo);
            Validacoes.ValidarSeVazio(Conteudo, ConteudoEmBrancoOuNulo);
            Validacoes.ValidarSeStringEntreCaracteres(Conteudo, MINIMO_CARACTERES_CONTEUDO, MAXIMO_CARACTERES_CONTEUDO, ConteudoSemMinimoDeCaracteres, ConteudoUltrapassouMaximoCaracteres);
        }
    }
}
