namespace LDCC.Models.Cuenta.DTO;

public class SaldoCuentaDto
{
    public int Id { get; set; }
    public int IdMoneda { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public string TipoCuenta { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public string CodigoMoneda { get; set; } = string.Empty;
    public string NombreMoneda { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
    public DateTime? FechaModificacion { get; set; }
}