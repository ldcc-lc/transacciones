using LDCC.Models.Cuenta.DTO;

namespace LDCC.Services.Interfaces;

public interface ICuentaService
{
    Task<List<SaldoCuentaDto>> ObtenerCuentasConSaldosAsync();
}