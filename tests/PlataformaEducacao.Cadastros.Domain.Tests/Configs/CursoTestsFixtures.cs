using Bogus;
using Bogus.DataSets;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;

namespace PlataformaEducacao.Cadastros.Domain.Tests.Configs
{
    [CollectionDefinition(nameof(CursoCollection))]
    public class CursoCollection : ICollectionFixture<CursoTestsFixtures>
    { }
    public class CursoTestsFixtures
    {

        private readonly Lorem _lorem = new Lorem("pt_BR");
        private readonly Faker _fakerRandom = new Faker();
        private readonly Faker<Curso> _faker = new Faker<Curso>("pt_BR");
        private readonly Faker<ConteudoProgramatico> _fakerConteudo = new Faker<ConteudoProgramatico>("pt_BR");



        public Curso GerarCursoValido()
        {
            var charsTitulo = _fakerRandom.Random.Int(Curso.MINIMO_CARACTERES_TITULO, Curso.MAXIMO_CARACTERES_TITULO);
            var charsDescicao = _fakerRandom.Random.Int(ConteudoProgramatico.MINIMO_CARACTERES_DESCRICAO, ConteudoProgramatico.MAXIMO_CARACTERES_DESCRICAO);

            return GerarCurso(charsTitulo, charsDescicao, 0, 100, 0.01m, 99999999.99m);
        }

        public Curso GerarCurso(int maxCharTitulo, int maxCharDescricao, int minCargaHoraria, int maxCargaHoraria, decimal minValorCurso, decimal maxValorCurso)
        {
            _fakerConteudo
                .CustomInstantiator(f => new ConteudoProgramatico(GerarTexto(maxCharDescricao), _fakerRandom.Random.Int(minCargaHoraria, maxCargaHoraria)));
            _faker
                .CustomInstantiator(f => new Curso(GerarTexto(maxCharTitulo), Math.Round(_fakerRandom.Random.Decimal(minValorCurso, maxValorCurso), 2), _fakerConteudo.Generate()));                        
            return _faker.Generate();
        }

      
        private string GerarTexto(int maxChar)
        {
            string texto = _lorem.Word();
            texto = texto.Substring(0, maxChar > texto.Length ? texto.Length : maxChar);
            while (texto.Length < maxChar)
            {
                texto += $" {_lorem.Word()}";
                texto = texto.Substring(0, maxChar > texto.Length ? texto.Length : maxChar);
            }
            return texto;
        }
    }

}
