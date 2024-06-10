using Localize.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Domain.Entidades
{
    public class Pedido : Base
    {
        public Pedido(string cnpj, string resultado)
        {
            Cnpj = cnpj;
            Resultado = resultado;
        }

        public string Cnpj { get; set; }
        public string Resultado { get; set; }
    }
}
