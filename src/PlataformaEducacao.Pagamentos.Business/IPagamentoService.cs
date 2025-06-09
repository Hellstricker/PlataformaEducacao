using PlataformaEducacao.Core.DomainObjects.Dtos;

namespace PlataformaEducacao.Pagamentos.Business
{
    public interface IPagamentoService
    {
        Task<Transacao> RealizarPagamentoCurso(PagamentoMatriculaDto pagamentoMatricula);
    }
}