using System.ComponentModel.DataAnnotations;

namespace Localize.API.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O nome não pode vazio")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "A senha não pode vazio")]
        public string? Senha { get; set; }
    }
}
