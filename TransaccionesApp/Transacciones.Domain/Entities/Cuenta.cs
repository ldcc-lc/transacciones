using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Domain.Enums;

namespace Transacciones.Domain.Entities
{
    public class Cuenta
    {
        public int Id { get; set; }
        public int MonedaId { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public TipoCuenta Tipo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal Saldo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
       
        public Moneda Moneda { get; set; } = null!;

        public ICollection<Movimiento> Movimientos { get; set; } = new List<Movimiento>();

    }
}
