namespace PlataformaEducacao.Core.Messages.Messages.IntegrationEvents
{
    public class CursoCadastradoEvent : IntegrationEvent
    {
        public Guid CursoId { get; private set; }
        public string Nome { get; private set; }
        public string Objetivo { get; private set; }
        public string Conteudo { get; private set; }
        public CursoCadastradoEvent(Guid cursoId, string nome, string objetivo, string conteudo)
        {
            AggregateId = cursoId;
            CursoId = cursoId;
            Nome = nome;
            Objetivo = objetivo;
            Conteudo = conteudo;
        }
    }
}
