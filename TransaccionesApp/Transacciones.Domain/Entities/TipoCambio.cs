using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transacciones.Domain.Entities
{
    public class TipoCambio
    {
        public int Id { get; set; }
        public int MonedaOrigenId { get; set; }
        public int MonedaDestinoId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Tasa { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public Moneda MonedaOrigen { get; set; } = null!;

        public Moneda MonedaDestino { get; set; } = null!;

    }
}
