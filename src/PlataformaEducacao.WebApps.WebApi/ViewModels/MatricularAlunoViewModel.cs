using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.WebApps.WebApi.ViewModels
{
    public class MatricularAlunoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é necessário")]
        public Guid CursoId { get; set; }
    }
}
