using PlataformaEducacao.Cadastros.Domain.Tests.Configs;
using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.Cadastros.Domain.Tests
{
    [Collection(nameof(AulaCollection))]
    public class AulaTests
    {
        private readonly AulaTestsFixtures _fixtures;

        public AulaTests(AulaTestsFixtures fixtures)
        {
            _fixtures = fixtures;
        }

        [Fact(DisplayName = "Criar Aula Com Titulo Em Branco Ou Nulo")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaSemTitulo_DeveGerarDomainException()
        {            
            // Arrange & Act
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarAula(0, Aula.MINIMO_CARACTERES_CONTEUDO, Aula.MINIMO_DURACAO, Aula.MINIMO_DURACAO + 1));
            // Assert
            Assert.Equal(Aula.TituloEmBrancoOuNulo, ex.Message);
        }

        [Fact(DisplayName = $"Criar Aula Com Titulo Sem Minimo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComTituloMenorQueMinimoCaracteres_DeveGerarDomainException()
        {
            // Arrange & Act
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarAula(Aula.MINIMO_CARACTERES_TITULO - 1, Aula.MINIMO_CARACTERES_CONTEUDO, Aula.MINIMO_DURACAO, Aula.MINIMO_DURACAO + 1));
            // Assert
            Assert.Equal(Aula.TituloSemMinimoDeCaracteres, ex.Message);
        }

        [Fact(DisplayName = $"Criar Aula Com Titulo Ultrapassando Maximo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComTituloMaiorQueMaximoCaracteres_DeveGerarDomainException()
        {
            // Arrange & Act
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarAula(Aula.MAXIMO_CARACTERES_TITULO + 1, Aula.MINIMO_CARACTERES_CONTEUDO, Aula.MINIMO_DURACAO, Aula.MINIMO_DURACAO + 1));
            // Assert
            Assert.Equal(Aula.TituloMaiorMaximoCaracteres, ex.Message);
        }


        [Fact(DisplayName = "Criar Aula Com Duracao Igual Zero")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComDuracaoIgualAZero_DeveGerarDomainException()
        {            
            // Arrange & Act
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarAula(Aula.MINIMO_CARACTERES_TITULO, Aula.MINIMO_CARACTERES_CONTEUDO, 0, 0));
            // Assert
            Assert.Equal(Aula.DuracaoDeveSerMaiorOuIgualMinimo, ex.Message);
        }

        [Fact(DisplayName = "Criar Aula Com Duracao Negativa")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComDuracaoNegativa_DeveGerarDomainException()
        {   
            // Arrange & Act
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarAula(Aula.MINIMO_CARACTERES_TITULO, Aula.MINIMO_CARACTERES_CONTEUDO, -1, 0));
            // Assert
            Assert.Equal(Aula.DuracaoDeveSerMaiorOuIgualMinimo, ex.Message);
        }
        
        [Fact(DisplayName = $"Criar Aula Com Conteudo Em Branco")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComConteudoEmBranco_DeveGerarDomainException()
        {
            // Arrange & Act
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarAula(Aula.MINIMO_CARACTERES_TITULO, 0, Aula.MINIMO_DURACAO, Aula.MINIMO_DURACAO + 1));
            // Assert
            Assert.Equal(Aula.ConteudoEmBrancoOuNulo, ex.Message);
        }

        [Fact(DisplayName = $"Criar Aula Com Conteudo Ultrapassando Maximo de Caracteres")]        
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComConteudoMaiorQueMaximoCaracteres_DeveGerarDomainException()
        {   
            // Arrange & Act
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarAula(Aula.MINIMO_CARACTERES_TITULO, Aula.MAXIMO_CARACTERES_CONTEUDO+1, Aula.MINIMO_DURACAO, Aula.MINIMO_DURACAO + 1));
            // Assert
            Assert.Equal(Aula.ConteudoUltrapassouMaximoCaracteres, ex.Message);
        }

        [Fact(DisplayName = $"Criar Aula Com Conteudo Sem Minimo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComConteudoSemQueMaximoCaracteres_DeveGerarDomainException()
        {            
            // Arrange & Act
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarAula(Aula.MINIMO_CARACTERES_TITULO, Aula.MINIMO_CARACTERES_CONTEUDO - 1, Aula.MINIMO_DURACAO, Aula.MINIMO_DURACAO + 1));
            // Assert
            Assert.Equal(Aula.ConteudoSemMinimoDeCaracteres, ex.Message);
        }

        [Fact(DisplayName = "Criar Aula Sem Falha")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaSemFalha_DeveGerarDomainException()
        {
            //Arrange & Act & Assert
            var ex = Record.Exception(() => _fixtures.GerarAulaValida());

            Assert.False(ex is DomainException, ex?.Message);
        }

    }
}
