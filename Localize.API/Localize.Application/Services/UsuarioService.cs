using AutoMapper;
using EscNet.Cryptography.Interfaces;
using Localize.Application.DTO;
using Localize.Application.Interfaces;
using Localize.Domain.Entities;
using Localize.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IRijndaelCryptography _rijndaelCryptography;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository, IRijndaelCryptography rijndaelCryptography)
        {
            this._mapper = mapper;
            this._usuarioRepository = usuarioRepository;
            this._rijndaelCryptography = rijndaelCryptography;
        }

        public async Task<List<UsuarioDTO>> Get()
        {
            var usuarios = await _usuarioRepository.Get();

            return _mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        public async Task<UsuarioDTO> GetById(long id)
        {
            var usuario = await _usuarioRepository.Get(id);

            return _mapper.Map<UsuarioDTO>(usuario);
        }

        public async Task<UsuarioDTO> Create(UsuarioDTO usuarioDTO)
        {
            var usuario = _mapper.Map<Usuario>(usuarioDTO);

            usuario.Senha = (_rijndaelCryptography.Encrypt(usuario.Senha));

            var usuarioCriado = await _usuarioRepository.Create(usuario);

            return _mapper.Map<UsuarioDTO>(usuarioCriado);
        }

        public async Task<UsuarioDTO> Update(UsuarioDTO usuarioDTO)
        {
            var usuarioExists = await _usuarioRepository.Get(usuarioDTO.Id);

            if (usuarioExists == null)
                throw new Exception("Não existe nenhum usuário com o id informado");

            var usuario = _mapper.Map<Usuario>(usuarioDTO);

            usuario.Senha = (_rijndaelCryptography.Encrypt(usuario.Senha));

            var usuarioUpdated = await _usuarioRepository.Update(usuario);

            return _mapper.Map<UsuarioDTO>(usuarioUpdated);
        }

        public async Task Remove(long id)
        {
            await _usuarioRepository.Remove(id);
        }

        public bool ValidaUsuario(Usuario usuario)
        {
            var resultado = _usuarioRepository.GetByName(usuario.Nome);

            usuario.Senha = (_rijndaelCryptography.Encrypt(usuario.Senha));

            if (usuario.Nome == resultado.Result.Nome && usuario.Senha == resultado.Result.Senha)
                return true;
            else
                return false;
        }
    }
}
