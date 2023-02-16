namespace webapi.interfaces;

public interface IDapperRepository
{
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null);
    Task<T> QueryFirstAsync<T>(string sql, object? param = null);
    Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string procedureName, object? param = null);
}
