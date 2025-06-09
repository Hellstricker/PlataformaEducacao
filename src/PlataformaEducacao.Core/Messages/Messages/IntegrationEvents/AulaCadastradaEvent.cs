namespace PlataformaEducacao.Core.Messages.Messages.IntegrationEvents
{
    public class AulaCadastradaEvent : IntegrationEvent
    {        
        public Guid CursoId { get; private set; }
        public Guid AulaId { get; private set; }
        public string Titulo { get; private set; }
        public string Conteudo { get; private set; }
        public int TotalMinutos { get; private set; }
        public AulaCadastradaEvent(Guid cursoId, Guid aulaId, string titulo, string conteudo, int totalMinutos)
        {
            AggregateId = cursoId;
            CursoId = cursoId;
            AulaId = aulaId;
            Titulo = titulo;
            Conteudo = conteudo;
            TotalMinutos = totalMinutos;
        } 
    }
}
