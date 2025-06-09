using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.GestaoCursos.Domain
{
    public class ConteudoProgramatico
    {
        private ConteudoProgramatico() { }

        public ConteudoProgramatico(string? objetivo, string? conteudo)
        {
            this.Objetivo = objetivo;
            this.Conteudo = conteudo;
            Validar();
        }

        public string? Objetivo { get; private set; }
        public string? Conteudo { get; private set; }

        private void Validar()
        {
            Validacoes.ValidarSeVazio(Objetivo, "Informe o objetivo do curso");
            Validacoes.ValidarSeVazio(Conteudo, "Informe o conteúdo do curso");
        }
    }
}
