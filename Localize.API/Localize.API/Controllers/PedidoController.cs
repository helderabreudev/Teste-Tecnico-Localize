using Localize.API.ViewModels;
using Localize.Application.Interfaces;
using Localize.Domain.Entidades;
using Localize.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Localize.API.Controllers
{
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IUsuarioService _usuarioService;


        public PedidoController(IPedidoService pedidoService, IUsuarioService usuarioService)
        {
            _pedidoService = pedidoService;
            _usuarioService = usuarioService;
        }

        [Authorize]
        [HttpPost("/api/v1/users/postpedido")] 
        public async Task<IActionResult> PostPedido([FromBody] CriarPedidoViewModel dadosPedido)
        {
            try
            {
                Usuario usuario = new(dadosPedido.Nome, dadosPedido.Senha);
                bool valido = _usuarioService.ValidaUsuario(usuario);
                 
                if (valido)
                {
                    var pedido = await _pedidoService.PostPedido(dadosPedido.Cnpj);

                    return Ok(new ResultadoViewModel
                    {
                        Mensagem = "Pedido Realizado com sucesso",
                        Successo = true,
                        Resultado = pedido
                    });
                }
                else
                {
                    return BadRequest(new ResultadoViewModel
                    {
                        Mensagem = "Usuário e senha incorretos",
                        Successo = false,
                        Resultado = null
                    });
                }
            }

            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet("/api/v1/users/getpedido/{id}")]
        public async Task<IActionResult> GetPedido(long id)
        {
            try
            {
                var pedido = await _pedidoService.GetPedido(id);

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Pedido Salvo com sucesso",
                    Successo = true,
                    Resultado = pedido
                });
            }

            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
