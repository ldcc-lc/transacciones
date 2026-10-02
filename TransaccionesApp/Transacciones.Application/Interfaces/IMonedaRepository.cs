using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Domain.Entities;

namespace Transacciones.Application.Interfaces
{
    public interface IMonedaRepository
    {
        Task<IEnumerable<Moneda>> GetAllAsync();
        Task<Moneda?> GetByIdAsync(int id);
    }
}
