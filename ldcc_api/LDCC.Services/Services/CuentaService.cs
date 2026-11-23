using LDCC.Models.Cuenta.DTO;
using LDCC.Repository.Interfaces;
using LDCC.Services.Interfaces;

namespace LDCC.Services.Services;

public class CuentaService : ICuentaService
{
    private readonly ICuentaRepository _cuentaRepository;

    public CuentaService(ICuentaRepository cuentaRepository)
    {
        _cuentaRepository = cuentaRepository;
    }

    public Task<List<SaldoCuentaDto>> ObtenerCuentasConSaldosAsync()
    {
        return _cuentaRepository.ObtenerCuentasConSaldosAsync();
    }
}