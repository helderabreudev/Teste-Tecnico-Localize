using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Localize.Application.DTO
{
    public class UsuarioDTO
    {
        public long Id { get; set; }
        public string? Nome { get; set; }

        [JsonIgnore] 
        public string? Senha { get; set; }

        public UsuarioDTO() { }

        public UsuarioDTO(long id, string? nome, string? senha)
        {
            this.Id = id;
            this.Nome = nome;
            this.Senha = senha;
        }
    }
}
