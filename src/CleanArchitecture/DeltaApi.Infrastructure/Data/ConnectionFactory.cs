using System.Data;
using Npgsql;
using Dapper;

namespace DeltaApi.Infrastructure.Data;

public class ConnectionFactory : IConnectionFactory
{
    private readonly string _connectionString;
    private static bool _isConfigured = false;
    private static readonly object _lock = new object();

    public ConnectionFactory(string connectionString)
    {
        _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        ConfigureDapper();
    }

    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);

    private static void ConfigureDapper()
    {
        if (_isConfigured) return;
        
        lock (_lock)
        {
            if (_isConfigured) return;
            
            // Configurar mapeo de tipos para PostgreSQL
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            _isConfigured = true;
        }
    }
}
