using AutoMapper;
using Localize.API.ViewModels;
using Localize.Application.DTO;
using Localize.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Localize.API.Controllers
{
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _userService;
        private readonly IMapper _mapper;

        public UsuarioController(IMapper mapper, IUsuarioService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("api/v1/users/get/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var user = await _userService.GetById(id);

                if (user == null)
                {
                    return Ok(new ResultadoViewModel
                    {
                        Mensagem = "Nenhum usuário encontrado com o id informado",
                        Successo = true,
                        Resultado = null
                    });
                }

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuário encontrado com sucesso",
                    Successo = true,
                    Resultado = user
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet("api/v1/users/get-all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var allUsuarios = await _userService.Get();

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuários encontrados com sucesso",
                    Successo = true,
                    Resultado = allUsuarios
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        
        [Authorize]
        [HttpPost("/api/v1/users/post")]
        public async Task<IActionResult> Create([FromBody] CriarUsuarioViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UsuarioDTO>(userViewModel);

                var userCreated = await _userService.Create(userDTO);

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuário criado com sucesso!",
                    Successo = true,
                    Resultado = userCreated
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        
        [Authorize]
        [HttpPut("api/v1/users/update")]
        public async Task<IActionResult> Update([FromBody] AtualizarUsuarioViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UsuarioDTO>(userViewModel);

                var userUpdated = await _userService.Update(userDTO);

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuário atualizado com sucesso",
                    Successo = true,
                    Resultado = userUpdated
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpDelete("api/v1/users/delete")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _userService.Remove(id);

                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuário removido com sucesso",
                    Successo = true,
                    Resultado = null
                });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
