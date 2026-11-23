using LDCC.Models.Cuenta.DTO;

namespace LDCC.Repository.Interfaces;

public interface ICuentaRepository
{
    Task<List<SaldoCuentaDto>> ObtenerCuentasConSaldosAsync();
}