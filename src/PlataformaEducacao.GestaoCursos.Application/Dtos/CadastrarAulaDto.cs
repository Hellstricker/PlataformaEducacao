using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.GestaoCursos.Application.Dtos
{
    public class CadastrarAulaDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Titulo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Conteudo { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int TotalMinutos { get; set; }
    }
}
