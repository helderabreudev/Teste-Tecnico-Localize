using Localize.Domain.Entities;
using Localize.Infra.Context;
using Localize.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Infra.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly LocalizeContext _context;
        public UsuarioRepository(LocalizeContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Get(long id)
        {
            var obj = await _context.Set<Usuario>()
                                    .AsNoTracking()
                                    .Where(x => x.Id == id)
                                    .ToListAsync();

            return obj.FirstOrDefault()!;
        }

        public async Task<List<Usuario>> Get()
        {
            return await _context.Set<Usuario>()
                                    .AsNoTracking()
                                    .ToListAsync();
        }

        public async Task<Usuario> Create(Usuario obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task<Usuario> Update(Usuario obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }

        public async Task Remove(long id)
        {
            var obj = await Get(id);

            if (obj != null)
            {
                _context.Remove(obj);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario> GetByName(string nome)
        {
            var obj = await _context.Set<Usuario>()
                        .AsNoTracking()
                        .Where(x => x.Nome == nome)
                        .ToListAsync();

            return obj.FirstOrDefault()!;
        }
    }
}
