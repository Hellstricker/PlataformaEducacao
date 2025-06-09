using PlataformaEducacao.Pagamentos.Business;
namespace PlataformaEducacao.Pagamentos.AntiCorruption
{
    public class PaypalGateway : IPaypalGateway
    {
        public Transacao CommitTransaction(string cardHashKey, string orderId, string paymentId, decimal amount)
        {
            var result = new Random().Next(1) == 0;
            var transacao = new Transacao
            {
                MatriculaId = Guid.Parse(orderId),
                Total = amount,
                PagamentoId = Guid.Parse(paymentId)
            };

            transacao.StatusTransacao = result? StatusTransacao.Pago : StatusTransacao.Recusado;
            
            return transacao;
        }

        public string GetCardHashKey(string serviceKey, string cartaoCredito)
        {
            return new string(Enumerable.Repeat("ABCCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(s => s[new Random().Next(s.Length)])
                .ToArray());
        }

        public string GetPaypalServiceKey(string apiKey, string encriptKey)
        {
            return new string(Enumerable.Repeat("ABCCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 10)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}
