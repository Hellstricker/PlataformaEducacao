using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.GestaoCursos.Application.Dtos
{
    public class CadastrarCursoDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Conteudo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Objetivo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do curso deve ser maior que zero")]
        public decimal Valor { get; set; }
    }
}
