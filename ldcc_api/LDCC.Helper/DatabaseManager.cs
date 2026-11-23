using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace LDCC.Helper;

public class DatabaseManager : IDatabaseManager
{
    private readonly string _connectionString;

    public DatabaseManager(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString("Transacciones")
            ?? throw new InvalidOperationException(
                "No se encontró la cadena de conexión 'Transacciones'.");
    }

    public async Task<DataTable> ExecuteStoredProcedureReturningDataAsync(
        string procedureName,
        List<SqlParameter> parameters)
    {
        var result = new DataTable();

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(procedureName, connection)
        {
            CommandType = CommandType.StoredProcedure
        };

        foreach (var parameter in parameters)
        {
            if (parameter.Value is null)
                parameter.Value = DBNull.Value;

            command.Parameters.Add(parameter);
        }

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();
        result.Load(reader);

        return result;
    }
}