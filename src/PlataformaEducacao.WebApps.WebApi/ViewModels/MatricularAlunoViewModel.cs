using PlataformaEducacao.Gestao.Domain;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.WebApps.WebApi.ViewModels
{
    public class MatricularAlunoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é necessário")]
        public Guid CursoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é necessário")]        
        public string CursoNome { get; set; }

        [Required(ErrorMessage = "O campo {0} é necessário")]        
        public decimal CursoValor { get; set; }

        [Required(ErrorMessage = "O campo {0} é necessário")]        
        public int CursoTotalAulas { get; set; } = 0;
    }
}
