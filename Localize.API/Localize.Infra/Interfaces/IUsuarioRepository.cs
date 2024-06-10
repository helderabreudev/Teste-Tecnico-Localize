using Localize.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Infra.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Create(Usuario usuario);
        Task<Usuario> Update(Usuario usuario);
        Task Remove(long id);
        Task<Usuario> Get(long id);
        Task<List<Usuario>> Get();
        Task<Usuario> GetByName(string nome);
    }
}
