namespace PlataformaEducacao.Pagamentos.Business
{
    public interface IPagamentoCartaoCreditoFacade
    {
        Transacao RealizarPagamento(Curso matricula, Pagamento pagamento);
    }
}