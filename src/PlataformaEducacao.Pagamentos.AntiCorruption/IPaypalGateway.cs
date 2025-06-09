using PlataformaEducacao.Pagamentos.Business;
namespace PlataformaEducacao.Pagamentos.AntiCorruption
{
    public interface IPaypalGateway
    {
        string GetPaypalServiceKey(string apiKey,string encriptKey);
        string GetCardHashKey(string serviceKey, string cartaoCredito);
        Transacao CommitTransaction(string cardHashKey, string orderId, string paymentId, decimal amount);
    }
}
