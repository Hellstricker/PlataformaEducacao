using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.Pagamentos.Business
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public string Status { get; set; }
        public decimal Valor { get; set; }
        public DadosCartao DadosCartao { get; set; }
        public Transacao Transacao { get; set; }        
    }
}