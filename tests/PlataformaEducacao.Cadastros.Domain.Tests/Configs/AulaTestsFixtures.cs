using Bogus;
using Bogus.DataSets;

namespace PlataformaEducacao.Cadastros.Domain.Tests.Configs
{
    public class AulaTestsFixtures
    {
        private readonly Lorem _lorem = new Lorem("pt_BR");
        private readonly Faker _fakerRandom = new Faker();
        private readonly Faker<Aula> _fakerAula = new Faker<Aula>("pt_BR");

        public Aula GerarAulaValida(int duracao)
        {
            if (duracao < Aula.MINIMO_DURACAO) duracao = Aula.MINIMO_DURACAO;
            return GerarAula(Aula.MINIMO_CARACTERES_TITULO, Aula.MINIMO_CARACTERES_CONTEUDO, duracao, duracao);
        }

        public Aula GerarAulaValida()
        {
            return GerarAula(Aula.MINIMO_CARACTERES_TITULO, Aula.MINIMO_CARACTERES_CONTEUDO, Aula.MINIMO_DURACAO, Aula.MINIMO_DURACAO);
        }

        public Aula GerarAula(int maxCharNome, int maxCharConteudo, int minDiracaoAula, int maxDuracaoAula)
        {
            _fakerAula
                .CustomInstantiator(f => new Aula(GerarTexto(maxCharNome), _fakerRandom.Random.Int(minDiracaoAula, maxDuracaoAula), GerarTexto(maxCharConteudo)));            
            return _fakerAula.Generate();
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
