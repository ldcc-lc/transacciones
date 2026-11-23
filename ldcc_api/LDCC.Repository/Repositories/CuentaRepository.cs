using System.Data;
using Microsoft.Data.SqlClient;
using LDCC.Helper;
using LDCC.Models.Cuenta.DTO;
using LDCC.Repository.Interfaces;

namespace LDCC.Repository.Repositories;

public class CuentaRepository : ICuentaRepository
{
    private readonly IDatabaseManager _databaseManager;

    public CuentaRepository(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }

    public async Task<List<SaldoCuentaDto>> ObtenerCuentasConSaldosAsync()
    {
        var parameters = new List<SqlParameter>();

        DataTable dataTable =
            await _databaseManager.ExecuteStoredProcedureReturningDataAsync(
                "dbo.usp_Cuenta_ObtenerSaldos",
                parameters);

        var cuentas = new List<SaldoCuentaDto>();

        foreach (DataRow row in dataTable.Rows)
        {
            var cuenta = new SaldoCuentaDto
            {
                NumeroCuenta = Convert.ToString(row["NumeroCuenta"]) ?? string.Empty,
                TipoCuenta = Convert.ToString(row["TipoCuenta"]) ?? string.Empty,
                Nombre = Convert.ToString(row["Nombre"]) ?? string.Empty,
                Saldo = Convert.ToDecimal(row["Saldo"]),
                CodigoMoneda = Convert.ToString(row["CodigoMoneda"]) ?? string.Empty,
                FechaCreacion = Convert.ToDateTime(row["FechaCreacion"])
            };

            if (row["FechaModificacion"] != DBNull.Value)
            {
                cuenta.FechaModificacion =
                    Convert.ToDateTime(row["FechaModificacion"]);
            }

            cuentas.Add(cuenta);
        }

        return cuentas;
    }
}