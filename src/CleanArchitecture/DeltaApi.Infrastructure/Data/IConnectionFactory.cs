using System.Data;

namespace DeltaApi.Infrastructure.Data;

public interface IConnectionFactory
{
    IDbConnection CreateConnection();
}
