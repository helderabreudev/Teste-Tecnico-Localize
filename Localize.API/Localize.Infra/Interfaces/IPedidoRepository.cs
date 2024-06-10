using Localize.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Infra.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> PostPedido(string cnpj);
        Task<Pedido> GetPedido(long id);

    }
}
