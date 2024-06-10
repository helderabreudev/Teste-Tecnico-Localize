using System.ComponentModel.DataAnnotations;

namespace Localize.API.ViewModels
{
    public class CriarPedidoViewModel
    {
        public string? Cnpj { get; set; }
        public string? Nome { get; set; }
        public string? Senha { get; set; }
    }
}
