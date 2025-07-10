using PlataformaEducacao.Cadastros.Domain.Tests.Configs;
using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.Cadastros.Domain.Tests
{
    [Collection(nameof(CursoCollection))]
    public class CursoTests
    {

        private readonly CursoTestsFixtures _fixtures;
        private readonly AulaTestsFixtures _aulaFixtures;

        public CursoTests(CursoTestsFixtures fixtures, AulaTestsFixtures aulaFixtures)
        {
            _fixtures = fixtures;
            _aulaFixtures = aulaFixtures;
        }


        [Fact(DisplayName = "Criar Curso Com Titulo Em Branco Ou Nulo")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoSemTitulo_DeveGerarDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarCurso(0, ConteudoProgramatico.MINIMO_CARACTERES_DESCRICAO, 0, 100, 0.01m, 99999999.99m));

            //Assert
            Assert.Equal(Curso.TituloEmBrancoOuNulo, ex.Message);
        }

        [Fact(DisplayName = "Criar Curso Com Valor Menor ou Igual a Zero")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComValorMenorOuIgualAZero_DeveGerarDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarCurso(Curso.MINIMO_CARACTERES_TITULO, ConteudoProgramatico.MINIMO_CARACTERES_DESCRICAO, 0, 100, -99999999.99m, 0.00m));

            //Assert
            Assert.Equal(Curso.ValorMenorOuIgualAZero, ex.Message);
        }


        [Fact(DisplayName = "Criar Curso Sem Informar Conteudo Programatico")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoSemConteudoProgramatico_DeveGerarDomainException()
        {
            //Arrange
            var curso = _fixtures.GerarCursoValido();            
            
            //Act
            var ex = Assert.Throws<DomainException>(() => new Curso(curso.Titulo, curso.Valor, null));

            // Assert
            Assert.Equal(Curso.ConteudoProgramaticoNulo, ex.Message);
        }

        [Fact(DisplayName = "Criar Curso Sem Descricao do Conteudo Programatico")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoSemDescricaoDoConteudoProgramatico_DeveGerarDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarCurso(Curso.MINIMO_CARACTERES_TITULO, 0, 0, 100, 0.01m, 99999999.99m));

            //Assert
            Assert.Equal(ConteudoProgramatico.DescricaoEmBrancoOuNulo, ex.Message);
        }

        [Fact(DisplayName = "Criar Curso Com Duracao do Conteudo Programatico Menor que Zero")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComDuracaoDoConteudoProgramaticoMenorQueZero_DeveGerarDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarCurso(Curso.MINIMO_CARACTERES_TITULO, ConteudoProgramatico.MINIMO_CARACTERES_DESCRICAO, -100, 0, 0.01m, 99999999.99m));

            //Assert
            Assert.Equal(ConteudoProgramatico.CargaHorariaMenorOuIgualAZero, ex.Message);
        }

        [Fact(DisplayName = "Criar Curso Com Duracao do Conteudo Programatico Igual a Zero")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComDuracaoDoConteudoProgramaticoIgualAZero_DeveGerarDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarCurso(Curso.MINIMO_CARACTERES_TITULO, ConteudoProgramatico.MINIMO_CARACTERES_DESCRICAO, 0, 0, 0.01m, 99999999.99m));

            //Assert
            Assert.Equal(ConteudoProgramatico.CargaHorariaMenorOuIgualAZero, ex.Message);
        }

        [Fact(DisplayName = $"Criar Curso Com Titulo Sem Minimo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComTituloMenorQueMinimoCaracteres_DeveGerarDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarCurso(Curso.MINIMO_CARACTERES_TITULO-1, ConteudoProgramatico.MINIMO_CARACTERES_DESCRICAO, 0, 100, 0.01m, 99999999.99m));

            //Assert
            Assert.Equal(Curso.TituloSemMinimoCaracteres, ex.Message);
        }
        
        [Fact(DisplayName = $"Criar Curso Com Titulo Ultrapassando Maximo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComTituloMaiorQueMaximoCaracteres_DeveGerarDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarCurso(Curso.MAXIMO_CARACTERES_TITULO + 1, ConteudoProgramatico.MINIMO_CARACTERES_DESCRICAO, 0, 100, 0.01m, 99999999.99m));

            //Assert
            Assert.Equal(Curso.TituloMaiorMaximoCaracteres, ex.Message);
        }


        [Fact(DisplayName = $"Criar Curso Com Descricao Sem Minimo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComDescricaoMenorQueMinimoCaracteres_DeveGerarDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarCurso(Curso.MINIMO_CARACTERES_TITULO, ConteudoProgramatico.MINIMO_CARACTERES_DESCRICAO - 1, 0, 100, 0.01m, 99999999.99m));

            //Assert
            Assert.Equal(ConteudoProgramatico.DescricaoSemMinimoCaracteres, ex.Message);
        }

        [Fact(DisplayName = $"Criar Curso Com Descricao Ultrapassando Maximo de Caracteres")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoComDescricaiMaiorQueMaximoCaracteres_DeveGerarDomainException()
        {
            // Act & Assert
            var ex = Assert.Throws<DomainException>(() => _fixtures.GerarCurso(Curso.MINIMO_CARACTERES_TITULO, ConteudoProgramatico.MAXIMO_CARACTERES_DESCRICAO + 1, 0, 100, 0.01m, 99999999.99m));

            //Assert
            Assert.Equal(ConteudoProgramatico.DescricaoMaiorMaximoCaracteres, ex.Message);
        }

        [Fact(DisplayName = "Criar Curso Sem Falha")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CriarCurso_CursoCorreto_NaoDeveGerarException()
        {
            //Arrange && Act 
            var ex = Record.Exception(() => _fixtures.GerarCursoValido());
            
            //Assert
            Assert.False(ex is DomainException, ex?.Message);
        }

        [Fact(DisplayName = "Adicionar Aula Nomes Iguais")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CursoAdicionarAula_AulaComNomeIgual_DeveGerarDomainException()
        {
            // Arrange
            var curso = _fixtures.GerarCursoValido();
            var aula = _aulaFixtures.GerarAulaValida();            
            curso.AdicionarAula(aula);

            // Act && Assert
            var ex = Assert.Throws<DomainException>(() => curso.AdicionarAula(aula));            
            Assert.Contains(Curso.AulaJaExiste, ex.Message);
        }


        [Fact(DisplayName = "Adicionar Aula Ultrapassa CargaHoraria")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CursoAdicionarAula_AulaUltrapassaCargaHoraria_DeveGerarDomainException()
        {
            // Arrange
            var curso = _fixtures.GerarCursoValido();            
            var aula = _aulaFixtures.GerarAulaValida(curso.ConteudoProgramatico.CargaHoraria + 1);
            
            // Act && Assert
            var ex = Assert.Throws<DomainException>(() => curso.AdicionarAula(aula));            
            Assert.Contains(Curso.AdicionarAulaUltrapassaCargaHoraria, ex.Message);            
        }

        [Fact(DisplayName = "Adicionar Aula com Sucesso")]
        [Trait("Categoria", "Domínio Cadastro Entidade Curso")]
        public void CursoAdicionarAula_AulaCorreta_AulaDeveConstarNaLista()
        {
            // Arrange
            var curso = _fixtures.GerarCursoValido();
            var aula = _aulaFixtures.GerarAulaValida(curso.ConteudoProgramatico.CargaHoraria);

            // Act 
            curso.AdicionarAula(aula);

            //Assert
            Assert.Contains(aula, curso.Aulas);
            Assert.Equal(aula.CursoId, curso.Id);
        }
    }
}