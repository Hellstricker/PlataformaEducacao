namespace PlataformaEducacao.Core.Data
{
    public interface IUnityOfWork
    {
        Task<bool> CommitAsync();
    }
}
