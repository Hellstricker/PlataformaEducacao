using PlataformaEducacao.Cadastros.Domain;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.Cadastros.Application.ViewModels
{
    public class CursoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(Curso.MAXIMO_CARACTERES_TITULO, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = Curso.MINIMO_CARACTERES_TITULO)]
        public string? Titulo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(ConteudoProgramatico.MAXIMO_CARACTERES_DESCRICAO, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = ConteudoProgramatico.MINIMO_CARACTERES_DESCRICAO)]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(1, Int32.MaxValue, ErrorMessage = "O campo {0} deve ser  maior que zero")]
        public int CargaHoraria { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O campo {0} deve ser maior que zero")]
        public decimal Valor { get; set; }
    }
}

