using Localize.Domain.Entidades;
using Localize.Domain.Entities;
using Localize.Infra.Context;
using Localize.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Infra.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly LocalizeContext _context;
        private readonly IConfiguration _config;
        private string receitaWS = string.Empty;
        public PedidoRepository(LocalizeContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
            receitaWS = _config.GetSection("Services:ReceitaWS").Value;
        }

        public async Task<Pedido> PostPedido(string cnpj)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string requestUri = $"{receitaWS}{cnpj}";

                    HttpResponseMessage response = await httpClient.GetAsync(requestUri);
                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();

                    Pedido pedido = new(cnpj, responseBody);

                    _context.Add(pedido);
                    await _context.SaveChangesAsync();

                    return pedido;
                }
            }

        public async Task<Pedido> GetPedido(long id)
        {
            var obj = await _context.Set<Pedido>()
                        .AsNoTracking()
                        .Where(x => x.Id == id)
                        .ToListAsync();

            return obj.FirstOrDefault()!;
        }
    }
    }

