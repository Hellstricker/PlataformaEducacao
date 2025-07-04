using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.Cadastros.Domain.Tests
{
    public class AulaTests
    {
        [Fact(DisplayName = "Criar Aula Com Titulo Em Branco Ou Nulo")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaSemTitulo_DeveGerarDomainException()
        {            
            //Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Aula(string.Empty, 10, "Conteudo Aula 1"));

            Assert.Equal(Aula.TituloEmBrancoOuNulo, ex.Message);

        }

        [Fact(DisplayName = $"Criar Aula Com Titulo Sem Minimo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComTituloMenorQueMinimoCaracteres_DeveGerarDomainException()
        {

            //Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Aula("Titu", 10, "Conteudo Aula 1"));

            Assert.Equal(Aula.TituloSemMinimoDeCaracteres, ex.Message);
        }

        [Fact(DisplayName = $"Criar Aula Com Titulo Ultrapassando Maximo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComTituloMaiorQueMaximoCaracteres_DeveGerarDomainException()
        {
            //Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Aula("Etiam posuere quam ac quam. Maecenas aliquet accumsan leo. Nullam dapibus fermentum ipsum. Etiam quis quam. Integer lacinia. Nulla est. Nulla turpis magna,", 10, "Conteudo Aula 1"));

            Assert.Equal(Aula.TituloMaiorMaximoCaracteres, ex.Message);
        }


        [Fact(DisplayName = "Criar Aula Com Duracao Igual Zero")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComDuracaoIgualAZero_DeveGerarDomainException()
        {
            //Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Aula("Titulo aula", 0 , "Conteudo Aula 1"));

            Assert.Equal(Aula.DuracaoDeveSerMaiorQueMinimo, ex.Message);
        }

        [Fact(DisplayName = "Criar Aula Com Duracao Negativa")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComDuracaoNegativa_DeveGerarDomainException()
        {
            //Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Aula("Titulo aula", -1, "Conteudo Aula 1"));

            Assert.Equal(Aula.DuracaoDeveSerMaiorQueMinimo, ex.Message);
        }
        
        [Fact(DisplayName = $"Criar Aula Com Conteudo Em Branco")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComConteudoEmBranco_DeveGerarDomainException()
        {
            //Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Aula("Titulo aula", 10, string.Empty));

            Assert.Equal(Aula.ConteudoEmBrancoOuNulo, ex.Message);
        }

        [Fact(DisplayName = $"Criar Aula Com Conteudo Ultrapassando Maximo de Caracteres")]        
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComConteudoMaiorQueMaximoCaracteres_DeveGerarDomainException()
        {
            //Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Aula("Titulo aula", 10, "In sem justo, commodo ut, suscipit at, pharetra vitae, orci. Duis sapien nunc, commodo et, interdum suscipit, sollicitudin et, dolor. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Aliquam id dolor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos hymenaeos. Mauris dictum facilisis augue. Fusce tellus. Pellentesque arcu. Maecenas fermentum, sem in pharetra pellentesque, velit turpis volutpat ante, in pharetra metus odio a"));

            Assert.Equal(Aula.ConteudoUltrapassouMaximoCaracteres, ex.Message);
        }

        [Fact(DisplayName = $"Criar Aula Com Conteudo Sem Minimo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaComConteudoSemQueMaximoCaracteres_DeveGerarDomainException()
        {
            //Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Aula("Titulo aula", 10, "Nam quis nu"));

            Assert.Equal(Aula.ConteudoSemMinimoDeCaracteres, ex.Message);
        }

        [Fact(DisplayName = "Criar Aula Sem Falha")]
        [Trait("Categoria", "Domínio Cadastro Entidade Aula")]
        public void CriarAula_AulaSemFalha_DeveGerarDomainException()
        {
            //Arrange & Act & Assert
            var ex = Record.Exception(() => new Aula("Titulo aula", 10, "Conteudo Aula 1"));

            Assert.False(ex is DomainException, ex?.Message);
        }

    }
}
