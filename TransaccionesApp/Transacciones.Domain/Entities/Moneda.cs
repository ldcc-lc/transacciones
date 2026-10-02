using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transacciones.Domain.Entities
{
    public class Moneda
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }


        public ICollection<Cuenta> Cuentas { get; set; } = new List<Cuenta>();

        public ICollection<TipoCambio> TiposCambioOrigen { get; set; } = new List<TipoCambio>();

        public ICollection<TipoCambio> TiposCambioDestino { get; set; } = new List<TipoCambio>();
    }
}
