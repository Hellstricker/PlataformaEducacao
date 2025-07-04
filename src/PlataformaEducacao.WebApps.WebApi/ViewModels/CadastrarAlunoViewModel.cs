using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.WebApps.WebApi.ViewModels
{
    public class CadastrarAlunoViewModel
    {
        //TODO: adicionar validações

        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string ConfirmarSenha { get; set; }
    }
}
