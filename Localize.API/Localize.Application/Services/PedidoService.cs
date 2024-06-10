using Localize.Application.Interfaces;
using Localize.Domain.Entidades;
using Localize.Infra.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Pedido> GetPedido(long id)
        {
            var pedido = await _pedidoRepository.GetPedido(id);

            return pedido;
        }

        public async Task<Pedido> PostPedido(string cnpj)
        {
            var pedido = await _pedidoRepository.PostPedido(cnpj);

            return pedido;
        }
    }
}
