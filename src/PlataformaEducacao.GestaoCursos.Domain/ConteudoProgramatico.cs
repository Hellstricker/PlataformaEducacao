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
        }

        public string? Objetivo { get; private set; }
        public string? Conteudo { get; private set; }
    }
}
