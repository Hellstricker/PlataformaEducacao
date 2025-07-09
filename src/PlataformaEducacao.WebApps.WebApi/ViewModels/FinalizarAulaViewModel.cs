using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.WebApps.WebApi.ViewModels
{
    public class FinalizarAulaViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid CursoId { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        public Guid AulaId { get; set; }
    }
}
