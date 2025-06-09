using PlataformaEducacao.Core.Data;
using PlataformaEducacao.Pagamentos.Business;

namespace PlataformaEducacao.Pagamentos.Data.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly PagamentosContext _context;

        public PagamentoRepository(PagamentosContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public void Adicionar(Pagamento pagamento)
        {
            _context.Pagamentos.Add(pagamento);
        }

        public void Adicionar(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
