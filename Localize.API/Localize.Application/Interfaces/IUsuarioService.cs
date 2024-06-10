using Localize.Application.DTO;
using Localize.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> Get();
        Task<UsuarioDTO> GetById(long id);
        Task<UsuarioDTO> Create(UsuarioDTO usuarioDTO);
        Task<UsuarioDTO> Update(UsuarioDTO usuarioDTO);
        Task Remove(long id);
        bool ValidaUsuario(Usuario usuario);
    }
}
