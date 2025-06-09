using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.WebApps.WebApi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O campo {0} é necessário")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        public string? Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "O campo {0} é necessário")]
        [MinLength(8, ErrorMessage = "O campo {0} deve conter, no mínimo, {1} caracteres")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }
    }
}
