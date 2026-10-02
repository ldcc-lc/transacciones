using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transacciones.Domain.Enums;

namespace Transacciones.Domain.Entities
{
    public class Movimiento
    {
        public int Id { get; set; }
        public int CuentaId { get; set; }
        public DateTime Fecha { get; set; }
        public TipoMovimiento Tipo { get; set; }
        public decimal Importe { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public Cuenta Cuenta { get; set; } = null!;

    }
}
