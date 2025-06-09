using PlataformaEducacao.Pagamentos.Business;
namespace PlataformaEducacao.Pagamentos.AntiCorruption
{
    public class PagamentoCartaoCreetitoFacade : IPagamentoCartaoCreditoFacade
    {
        private readonly IPaypalGateway _paypalGateway;
        private readonly IConfigurationManager _configurationManager;

        public PagamentoCartaoCreetitoFacade(IPaypalGateway paypalGateway, IConfigurationManager configurationManager)
        {
            _paypalGateway = paypalGateway;
            _configurationManager = configurationManager;
        }
        public Transacao RealizarPagamento(Matricula matricula, Pagamento pagamento)
        {
            var apiKey = _configurationManager.GetValue("apiKey");
            var encriptionKey = _configurationManager.GetValue("encriptionKey");

            var serviceKey = _paypalGateway.GetPaypalServiceKey(apiKey, encriptionKey);
            var cardHashKey = _paypalGateway.GetCardHashKey(serviceKey, pagamento.DadosCartao.ToString());

            return _paypalGateway.CommitTransaction(cardHashKey, matricula.Id.ToString(), pagamento.Id.ToString(), pagamento.Valor);
        }
    }
}
