namespace PlataformaEducacao.WebApps.WebApi.ViewModels
{
    public class BaseResultViewModel
    {
        public bool Success { get; set; }
        public object? Data { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
