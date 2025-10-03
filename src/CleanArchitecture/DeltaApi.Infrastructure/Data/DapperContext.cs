using Npgsql;
using System.Data;
using Dapper;

namespace DeltaApi.Infrastructure.Data;

public class DapperContext
{
    private readonly string _connectionString;
    private static bool _isConfigured = false;
    private static readonly object _lock = new object();

    public DapperContext(string connectionString)
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
            Console.WriteLine("Registrando GuidTypeHandler...");
            SqlMapper.AddTypeHandler(new GuidTypeHandler());
            Console.WriteLine("GuidTypeHandler registrado exitosamente");
            _isConfigured = true;
        }
    }
}

