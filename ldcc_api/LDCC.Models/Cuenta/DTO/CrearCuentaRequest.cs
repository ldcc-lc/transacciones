namespace LDCC.Models.Cuenta.DTO;

public class CrearCuentaRequest
{
    public string NumeroCuenta { get; set; } = string.Empty;
    public string TipoCuenta { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string IdMoneda { get; set; } = string.Empty;
}