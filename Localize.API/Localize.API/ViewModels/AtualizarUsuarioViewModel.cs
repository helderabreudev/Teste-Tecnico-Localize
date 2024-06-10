using System.ComponentModel.DataAnnotations;

namespace Localize.API.ViewModels
{
    public class AtualizarUsuarioViewModel
    {
        [Required(ErrorMessage = "O Id não pode ser nulo")]
        [Range(1, long.MaxValue, ErrorMessage = "O id não pode ser menor que 1")]
        public long Id { get; set; }
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A senha não pode ser vazia")]
        public string? Senha { get; set; }
    }
}
