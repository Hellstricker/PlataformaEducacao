using PlataformaEducacao.Cadastros.Domain;
using PlataformaEducacao.Core.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.Cadastros.Application.ViewModels
{
    public class AulaViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [NonEmptyGuidAttribute(ErrorMessage = "O campo {0} deve ser um GUID válido e não nulo")]
        public Guid CursoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(Aula.MAXIMO_CARACTERES_TITULO, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = Aula.MINIMO_CARACTERES_TITULO)]
        public string? Titulo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(1, Int32.MaxValue, ErrorMessage = "O campo {0} deve ser  maior que zero")]
        public int Duracao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(Aula.MAXIMO_CARACTERES_CONTEUDO, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = Aula.MINIMO_CARACTERES_CONTEUDO)]
        public string? Conteudo { get; set; }
    }
}
