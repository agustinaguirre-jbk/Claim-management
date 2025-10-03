using Dapper;
using DeltaApi.Domain.Claims;
using DeltaApi.Domain.Repositories;
using DeltaApi.Infrastructure.Data;
using DeltaApi.Infrastructure.Mappings;

namespace DeltaApi.Infrastructure.Repositories;

public class StateOfLossRepository : IStateOfLossRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public StateOfLossRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async Task<StateOfLoss?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                state_of_loss_id, state_name, state_code,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            FROM claims.state_of_loss 
            WHERE state_of_loss_id = @Id AND deleted = false";

        using var connection = _connectionFactory.CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { Id = id });
        
        return result != null ? StateOfLossMapper.MapToStateOfLoss(result) : null;
    }

    public async Task<StateOfLoss?> GetByStateCodeAsync(string stateCode, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                state_of_loss_id, state_name, state_code,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            FROM claims.state_of_loss 
            WHERE state_code = @StateCode AND deleted = false";

        using var connection = _connectionFactory.CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { StateCode = stateCode });
        
        return result != null ? StateOfLossMapper.MapToStateOfLoss(result) : null;
    }

    public async Task<IEnumerable<StateOfLoss>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                state_of_loss_id, state_name, state_code,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            FROM claims.state_of_loss 
            WHERE deleted = false
            ORDER BY state_name";

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<dynamic>(sql);
        
        return results.Select(StateOfLossMapper.MapToStateOfLoss);
    }

    public async Task<StateOfLoss> AddAsync(StateOfLoss stateOfLoss, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO claims.state_of_loss (
                state_of_loss_id, state_name, state_code,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            ) VALUES (
                @StateOfLossId, @StateName, @StateCode,
                @Deleted, @CreatedByUser, @CreatedOn, @ModifiedByUser, @ModifiedOn
            )";

        var parameters = new
        {
            StateOfLossId = stateOfLoss.Id,
            StateName = stateOfLoss.StateName,
            StateCode = stateOfLoss.StateCode,
            Deleted = stateOfLoss.Deleted,
            CreatedByUser = stateOfLoss.CreatedByUser,
            CreatedOn = stateOfLoss.CreatedOn,
            ModifiedByUser = stateOfLoss.ModifiedByUser,
            ModifiedOn = stateOfLoss.ModifiedOn
        };

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
        
        return stateOfLoss;
    }

    public async Task UpdateAsync(StateOfLoss stateOfLoss, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE claims.state_of_loss SET
                state_name = @StateName,
                state_code = @StateCode,
                modified_by_user = @ModifiedByUser,
                modified_on = @ModifiedOn
            WHERE state_of_loss_id = @StateOfLossId";

        var parameters = new
        {
            StateOfLossId = stateOfLoss.Id,
            StateName = stateOfLoss.StateName,
            StateCode = stateOfLoss.StateCode,
            ModifiedByUser = stateOfLoss.ModifiedByUser,
            ModifiedOn = stateOfLoss.ModifiedOn
        };

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = "UPDATE claims.state_of_loss SET deleted = true WHERE state_of_loss_id = @Id";

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
