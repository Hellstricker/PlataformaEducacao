namespace PlataformaEducacao.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }


}
