using Dapper;
using DeltaApi.Domain.Claims;
using DeltaApi.Domain.Repositories;
using DeltaApi.Infrastructure.Data;
using DeltaApi.Infrastructure.Mappings;

namespace DeltaApi.Infrastructure.Repositories;

public class ClaimTypeRepository : IClaimTypeRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public ClaimTypeRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async Task<ClaimType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                claim_type_id, type_name, description, is_active,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            FROM claims.claim_type 
            WHERE claim_type_id = @Id AND deleted = false";

        using var connection = _connectionFactory.CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { Id = id });
        
        return result != null ? ClaimTypeMapper.MapToClaimType(result) : null;
    }

    public async Task<IEnumerable<ClaimType>> GetAllActiveAsync(CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                claim_type_id, type_name, description, is_active,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            FROM claims.claim_type 
            WHERE is_active = true AND deleted = false
            ORDER BY type_name";

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<dynamic>(sql);
        
        return results.Select(ClaimTypeMapper.MapToClaimType);
    }

    public async Task<IEnumerable<ClaimType>> SearchByDescriptionAsync(string description, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                claim_type_id, type_name, description, is_active,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            FROM claims.claim_type 
            WHERE type_name LIKE @Description AND deleted = false";

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<dynamic>(sql, new { Description = $"%{description}%" });
        
        return results.Select(ClaimTypeMapper.MapToClaimType);
    }

    public async Task<ClaimType> AddAsync(ClaimType claimType, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO claims.claim_type (
                claim_type_id, type_name, description, is_active,
                deleted, created_by_user, created_on, modified_by_user, modified_on
            ) VALUES (
                @ClaimTypeId, @TypeName, @Description, @IsActive,
                @Deleted, @CreatedByUser, @CreatedOn, @ModifiedByUser, @ModifiedOn
            )";

        var parameters = new
        {
            ClaimTypeId = claimType.Id,
            TypeName = claimType.ClaimTypeDescription,
            Description = claimType.ClaimTypeShortCode,
            IsActive = claimType.IsActive,
            Deleted = claimType.Deleted,
            CreatedByUser = claimType.CreatedByUser,
            CreatedOn = claimType.CreatedOn,
            ModifiedByUser = claimType.ModifiedByUser,
            ModifiedOn = claimType.ModifiedOn
        };

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
        
        return claimType;
    }

    public async Task UpdateAsync(ClaimType claimType, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE claims.claim_type SET
                type_name = @TypeName,
                description = @Description,
                is_active = @IsActive,
                modified_by_user = @ModifiedByUser,
                modified_on = @ModifiedOn
            WHERE claim_type_id = @ClaimTypeId";

        var parameters = new
        {
            ClaimTypeId = claimType.Id,
            TypeName = claimType.ClaimTypeDescription,
            Description = claimType.ClaimTypeShortCode,
            IsActive = claimType.IsActive,
            ModifiedByUser = claimType.ModifiedByUser,
            ModifiedOn = claimType.ModifiedOn
        };

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = "UPDATE claims.claim_type SET deleted = true WHERE claim_type_id = @Id";

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
