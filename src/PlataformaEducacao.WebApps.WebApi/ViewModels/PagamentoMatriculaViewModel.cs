using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.WebApps.WebApi.ViewModels
{
    public class PagamentoMatriculaViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid MatriculaId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? NomeCartao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? NumeroCartao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? MesAnoExpiracao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public string? Ccv { get; set; }
    }
}
