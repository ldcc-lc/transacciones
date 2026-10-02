using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transacciones.Application.Interfaces;
using Transacciones.Domain.Entities;
using Transacciones.Infrastructure.Data;

namespace Transacciones.Infrastructure.Repositories
{
    public class MonedaRepository : IMonedaRepository
    {
        private readonly TransaccionesDbContext _context;

        public MonedaRepository(TransaccionesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Moneda>> GetAllAsync()
        {
            return await _context.Monedas
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Moneda?> GetByIdAsync(int id)
        {
            return await _context.Monedas
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
