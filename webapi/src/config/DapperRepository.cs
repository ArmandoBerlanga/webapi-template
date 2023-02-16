namespace webapi.config;

using System.Data;
using Dapper;
using webapi.interfaces;


public class DapperRepository : IDapperRepository
{
    private readonly IDbConnection _connection;

    public DapperRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null)
    {
        return await _connection.QueryAsync<T>(sql, param);
    }

    public async Task<T> QueryFirstAsync<T>(string sql, object? param = null)
    {
        return await _connection.QueryFirstAsync<T>(sql, param);
    }

    public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string procedureName, object? param = null)
    {
        return await _connection.QueryAsync<T>(procedureName, param, commandType: CommandType.StoredProcedure);
    }
}
