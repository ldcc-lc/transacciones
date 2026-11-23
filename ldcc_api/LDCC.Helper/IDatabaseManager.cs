using System.Data;
using Microsoft.Data.SqlClient;

namespace LDCC.Helper;

public interface IDatabaseManager
{
    Task<DataTable> ExecuteStoredProcedureReturningDataAsync(
        string procedureName,
        List<SqlParameter> parameters);
}