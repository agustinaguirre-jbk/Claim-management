using System.Data;
using Dapper;
using Npgsql;

namespace DeltaApi.Infrastructure.Data;

public class GuidTypeHandler : Dapper.SqlMapper.TypeHandler<Guid>
{
    public override void SetValue(IDbDataParameter parameter, Guid value)
    {
        parameter.Value = value;
        parameter.DbType = DbType.Guid;
        
        // Forzar el tipo específico de PostgreSQL
        if (parameter is NpgsqlParameter npgsqlParam)
        {
            npgsqlParam.NpgsqlDbType = NpgsqlTypes.NpgsqlDbType.Uuid;
        }
    }

    public override Guid Parse(object value)
    {
        if (value == null || value == DBNull.Value)
            return Guid.Empty;
            
        if (value is Guid guid)
            return guid;
            
        if (value is string str && Guid.TryParse(str, out var parsedGuid))
            return parsedGuid;
            
        return (Guid)value;
    }
}
