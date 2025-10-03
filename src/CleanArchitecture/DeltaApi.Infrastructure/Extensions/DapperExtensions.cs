using Dapper;
using System.Data;

namespace DeltaApi.Infrastructure.Extensions;

public static class DapperExtensions
{
    public static async Task<T?> QueryFirstOrDefaultAsync<T>(this IDbConnection connection, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public static async Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection connection, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    public static async Task<int> ExecuteAsync(this IDbConnection connection, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
    }

    public static async Task<T> QuerySingleAsync<T>(this IDbConnection connection, string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await connection.QuerySingleAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }
}
