using System.ComponentModel.DataAnnotations;

namespace Localize.API.ViewModels
{
    public class CriarUsuarioViewModel
    {
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A senha não pode ser vazia")]
        public string? Senha { get; set; }
    }
}
