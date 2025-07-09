using PlataformaEducacao.Pagamentos.Business;
namespace PlataformaEducacao.Pagamentos.AntiCorruption
{
    public class PagamentoCartaoCreatitonFacade : IPagamentoCartaoCreditoFacade
    {
        private readonly IPaypalGateway _paypalGateway;
        private readonly IConfigurationManager _configurationManager;

        public PagamentoCartaoCreatitonFacade(IPaypalGateway paypalGateway, IConfigurationManager configurationManager)
        {
            _paypalGateway = paypalGateway;
            _configurationManager = configurationManager;
        }
        public Transacao RealizarPagamento(Curso curso, Pagamento pagamento)
        {
            var apiKey = _configurationManager.GetValue("apiKey");
            var encriptionKey = _configurationManager.GetValue("encriptionKey");

            var serviceKey = _paypalGateway.GetPaypalServiceKey(apiKey, encriptionKey);
            var cardHashKey = _paypalGateway.GetCardHashKey(serviceKey, pagamento.DadosCartao.ToString());

            return _paypalGateway.CommitTransaction(cardHashKey, curso.Id.ToString(), pagamento.Id.ToString(), pagamento.Valor);
        }
    }
}
