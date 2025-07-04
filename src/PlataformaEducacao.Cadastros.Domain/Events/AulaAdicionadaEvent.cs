using PlataformaEducacao.Core.Messages;

namespace PlataformaEducacao.Cadastros.Domain.Events
{
    public class AulaAdicionadaEvent : Event
    {
        public Guid CursoId { get; private set; }
        public string Titulo { get; private set; }
        public int Duracao { get; private set; }
        public string Conteudo { get; private set; }

        public AulaAdicionadaEvent(Guid cursoId, string titulo, int duracao, string conteudo)
        {
            CursoId = cursoId;
            Titulo = titulo;
            Duracao = duracao;
            Conteudo = conteudo;
        }
    }
}
