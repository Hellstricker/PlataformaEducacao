using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.Cadastros.Domain.Tests
{
    public class CursoTests
    {
        [Fact(DisplayName = "Criar Curso Com Titulo Em Branco Ou Nulo")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoSemTitulo_DeveGerarDomainException()
        {
            // Arrange
            var conteudoProgramatico = new ConteudoProgramatico("Test-Driven Development", 300);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Curso(string.Empty, 0, conteudoProgramatico));

            Assert.Equal(Curso.TituloEmBrancoOuNulo, ex.Message);
        }

        [Fact(DisplayName = "Criar Curso Com Valor Menor ou Igual a Zero")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComValorMenorOuIgualAZero_DeveGerarDomainException()
        {
            // Arrange
            var conteudoProgramatico = new ConteudoProgramatico("Test-Driven Development", 300);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Curso("Nome Curso", 0, conteudoProgramatico));

            Assert.Equal(Curso.ValorMenorOuIgualAZero, ex.Message);
        }


        [Fact(DisplayName = "Criar Curso Sem Informar Conteudo Programatico")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoSemConteudoProgramatico_DeveGerarDomainException()
        {
            //Arrange & Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Curso("Nome Curso", 10, null));

            Assert.Equal(Curso.ConteudoProgramaticoNulo, ex.Message);
        }

        [Fact(DisplayName = "Criar Curso Sem Descricao do Conteudo Programatico")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoSemDescricaoDoConteudoProgramatico_DeveGerarDomainException()
        {
            // Arrange
            var conteudoProgramatico = new ConteudoProgramatico(string.Empty, 300);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Curso("Nome Curso", 10, conteudoProgramatico));

            Assert.Equal(ConteudoProgramatico.DescricaoEmBrancoOuNulo, ex.Message);
        }

        [Fact(DisplayName = "Criar Curso Com Duracao do Conteudo Programatico Menor que Zero")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComDuracaoDoConteudoProgramaticoMenorQueZero_DeveGerarDomainException()
        {
            // Arrange
            var conteudoProgramatico = new ConteudoProgramatico("Test-Driven Development", -1);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Curso("Nome Curso", 10, conteudoProgramatico));

            Assert.Equal(ConteudoProgramatico.CargaHorariaMenorOuIgualAZero, ex.Message);
        }

        [Fact(DisplayName = "Criar Curso Com Duracao do Conteudo Programatico Igual a Zero")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComDuracaoDoConteudoProgramaticoIgualAZero_DeveGerarDomainException()
        {
            // Arrange
            var conteudoProgramatico = new ConteudoProgramatico("Test-Driven Development", 0);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Curso("Nome Curso", 10, conteudoProgramatico));

            Assert.Equal(ConteudoProgramatico.CargaHorariaMenorOuIgualAZero, ex.Message);
        }

        [Fact(DisplayName = $"Criar Curso Com Titulo Sem Minimo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComTituloMenorQueMinimoCaracteres_DeveGerarDomainException()
        {
            // Arrange
            var conteudoProgramatico = new ConteudoProgramatico("Test-Driven Development", 300);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Curso("Nome Curs", 10, conteudoProgramatico));

            Assert.Equal(Curso.TituloSemMinimoCaracteres, ex.Message);
        }
        
        [Fact(DisplayName = $"Criar Curso Com Titulo Ultrapassando Maximo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComTituloMaiorQueMaximoCaracteres_DeveGerarDomainException()
        {
            // Arrange
            var conteudoProgramatico = new ConteudoProgramatico("Test-Driven Development", 300);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Curso("Etiam posuere quam ac quam. Maecenas aliquet accumsan leo. Nullam dapibus fermentum ipsum. Etiam quis quam. Integer lacinia. Nulla est. Nulla turpis magna,", 10, conteudoProgramatico));

            Assert.Equal(Curso.TituloMaiorMaximoCaracteres, ex.Message);
        }


        [Fact(DisplayName = $"Criar Curso Com Descricao Sem Minimo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComDescricaoMenorQueMinimoCaracteres_DeveGerarDomainException()
        {
            // Arrange
            var conteudoProgramatico = new ConteudoProgramatico("Test-Driv", 300);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Curso("Nome Curso", 10, conteudoProgramatico));

            Assert.Equal(ConteudoProgramatico.DescricaoSemMinimoCaracteres, ex.Message);
        }

        [Fact(DisplayName = $"Criar Curso Com Descricao Ultrapassando Maximo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComDescricaiMaiorQueMaximoCaracteres_DeveGerarDomainException()
        {
            // Arrange
            var conteudoProgramatico = new ConteudoProgramatico("Nam quis nulla. Integer malesuada. In in enim a arcu imperdiet malesuada. Sed vel lectus. Donec odio urna, tempus molestie, porttitor ut, iaculis quis, sem. Phasellus rhoncus. Aenean id metus id velit ullamcorper pulvinar. Vestibulum fermentum tortor id mi. Pellentesque ipsum. Nulla non arcu lacinia neque faucibus fringilla. Nulla non lectus sed nisl molestie malesuada. Proin in tellus sit amet nibh dignissim sagittis. Vivamus luctus egestas leo. Maecenas sollicitudin. Nullam rhoncus aliquam metus. Etiam egestas", 300);

            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => new Curso("Nome Curso", 10, conteudoProgramatico));

            Assert.Equal(ConteudoProgramatico.DescricaoMaiorMaximoCaracteres, ex.Message);
        }

        [Fact(DisplayName = "Criar Curso Sem Falha")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoCorreto_NaoDeveGerarException()
        {
            // Arrange
            var conteudoProgramatico = new ConteudoProgramatico("Test-Driven Development", 300);

            // Act & Assert
            var ex = Record.Exception(() => new Curso("Nome curso", 10, conteudoProgramatico));

            Assert.False(ex is DomainException, ex?.Message);
        }

        [Fact(DisplayName = "Adicionar Aula Nomes Iguais")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CursoAdicionarAula_AulaComNomeIgual_DeveGerarDomainException()
        {
            // Arrange
            var curso = new Curso("Curso de Test-Driven Development", 10, new ConteudoProgramatico("Conteudo Programatico", 300));
            var aula1 = new Aula("Introdução ao Test-Driven Development", 60, "Conteudo da aula 1");
            var aula2 = new Aula("Introdução ao Test-Driven Development", 30, "Conteudo da aula 2");            
            curso.AdicionarAula(aula1);

            // Act 
            var ex = Assert.Throws<DomainException>(() => curso.AdicionarAula(aula2));

            //Assert
            Assert.Contains(Curso.AulaJaExiste, ex.Message);
        }


        [Fact(DisplayName = "Adicionar Aula Ultrapassa CargaHoraria")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CursoAdicionarAula_AulaUltrapassaCargaHoraria_DeveGerarDomainException()
        {
            // Arrange
            int cargaHoraria = 300;
            var curso = new Curso("Curso de Test-Driven Development", 100, new ConteudoProgramatico("Conteudo Programatico", cargaHoraria));
            var aula1 = new Aula("Introdução ao Test-Driven Development", cargaHoraria + 1, "Conteudo da aula 1");

            // Act 
            var ex = Assert.Throws<DomainException>(() => curso.AdicionarAula(aula1));

            //Assert
            Assert.Contains(Curso.AdicionarAulaUltrapassaCargaHoraria, ex.Message);
        }

        [Fact(DisplayName = "Aula Adicionada A Curso")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CursoAdicionarAula_AulaCorreta_NaoDeveGerarDomainException()
        {
            // Arrange
            int cargaHoraria = 300;
            var curso = new Curso("Curso de Test-Driven Development", 100, new ConteudoProgramatico("Conteudo Programatico", cargaHoraria));
            var aula1 = new Aula("Introdução ao Test-Driven Development", cargaHoraria, "Conteudo da aula 1");

            // Act 
            var ex = Record.Exception(() => curso.AdicionarAula(aula1));

            //Assert
            Assert.False(ex is DomainException , ex?.Message);
        }
    }
}