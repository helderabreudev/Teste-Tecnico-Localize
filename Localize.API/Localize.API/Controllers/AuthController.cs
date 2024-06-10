﻿using Localize.API.Token;
using Localize.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Localize.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenGenerator _tokenGenerator;

        public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator)
        {
            _configuration = configuration;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        [Route("/api/v1/auth/login")]
        public IActionResult Login([FromBody] LoginViewModel loginViewModel)
        {
            var tokenLogin = _configuration["Jwt:Login"];
            var tokenPassword = _configuration["Jwt:Password"];

            if (loginViewModel.Nome == tokenLogin && loginViewModel.Senha == tokenPassword)
            {
                return Ok(new ResultadoViewModel
                {
                    Mensagem = "Usuário autenticado com sucesso",
                    Successo = true,
                    Resultado = new
                    {
                        Token = _tokenGenerator.GenerateToken(),
                        TokenExpires = DateTime.UtcNow.AddHours(int.Parse((_configuration["Jwt:HoursToExpire"])))
                    }
                });
            }
            else
            {
                return StatusCode(401);
            }
        }
    }
}
