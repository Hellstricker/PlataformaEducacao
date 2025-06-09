using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.WebApps.WebApi.ViewModels
{
    public class CadastrarUsuarioViewModel
    {
        [Required(ErrorMessage = "O campo {0} é necessário")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é necessário")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres")]
        [EmailAddress(ErrorMessage = "{0} inválido")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é necessário")]
        [MinLength(8, ErrorMessage = "O campo {0} precisa ter no mínimo {1} caracteres")]
        [DataType(DataType.Password)]
        public string? Senha { get; set; }

        [Required(ErrorMessage = "O campo {0} é necessário")]
        [Compare("Senha", ErrorMessage = "As senhas não conferem")]
        public string? ConfirmarSenha { get; set; }
    }
}
