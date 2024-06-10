using Localize.Domain.Entidades;
using Localize.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Infra.Context
{
    public class LocalizeContext : DbContext
    {
        public LocalizeContext(DbContextOptions<LocalizeContext> options) : base(options) { }

        public virtual DbSet<Pedido> Pedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PedidoMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }
    }
}
