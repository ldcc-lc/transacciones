using LDCC.Models.Cuenta.DTO;
using LDCC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LDCC.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly ICuentaService _cuentaService;

    public CuentasController(ICuentaService cuentaService)
    {
        _cuentaService = cuentaService;
    }

    [HttpGet("saldos")]
    public async Task<ActionResult<List<SaldoCuentaDto>>>
        ObtenerCuentasConSaldosAsync()
    {
        var cuentas = await _cuentaService.ObtenerCuentasConSaldosAsync();
        return Ok(cuentas);
    }
}