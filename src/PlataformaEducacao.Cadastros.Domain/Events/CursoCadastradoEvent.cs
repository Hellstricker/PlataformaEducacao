using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.DomainEvents;
using PlataformaEducacao.Core.Messages.IntegrationEvents;

namespace PlataformaEducacao.Cadastros.Domain.Events
{
    public class CursoCadastradoEvent: IntegrationEvent
    {        
        public Guid CursoId { get; set; }
        public string Titulo { get; private set; }
        public decimal Valor { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }

        public CursoCadastradoEvent(Guid cursoId, string titulo, decimal valor, string descricao, int cargaHoraria)            
        {
            AggregateId = cursoId;
            CursoId = cursoId;
            Titulo = titulo;
            Valor = valor;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
        }
    }
}
