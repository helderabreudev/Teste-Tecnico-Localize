using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Domain.Entities
{
    public class Usuario : Base
    {
        public Usuario(string? nome, string? senha)
        {
            Nome = nome;
            Senha = senha;
        }

        public string? Nome { get; set; }
        public string? Senha { get; set; }
    }
}
