using Dapper;
using DeltaApi.Domain.Claims;
using DeltaApi.Domain.Repositories;
using DeltaApi.Infrastructure.Data;
using DeltaApi.Infrastructure.Mappings;

namespace DeltaApi.Infrastructure.Repositories;

public class ClaimRepository : IClaimRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public ClaimRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
    }

    public async Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                claim_id, case_id, claim_type_id, claimant_id, client_id,
                policy_number, delta_file_number, client_file_number,
                doctor_id, state_of_loss_id, alleged_injury, injury_description,
                attorney_representation, liability, workers_compensation, exposure
            FROM claims.claim 
            WHERE claim_id = @Id";

        using var connection = _connectionFactory.CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { Id = id });
        
        return result != null ? ClaimMapper.MapToClaim(result) : null;
    }

    public async Task<Claim?> GetByCaseIdAsync(int caseId, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                claim_id, case_id, claim_type_id, claimant_id, client_id,
                policy_number, delta_file_number, client_file_number,
                doctor_id, state_of_loss_id, alleged_injury, injury_description,
                attorney_representation, liability, workers_compensation, exposure
            FROM claims.claim 
            WHERE case_id = @CaseId";

        using var connection = _connectionFactory.CreateConnection();
        var result = await connection.QueryFirstOrDefaultAsync<dynamic>(sql, new { CaseId = caseId });
        
        return result != null ? ClaimMapper.MapToClaim(result) : null;
    }

    public async Task<IEnumerable<Claim>> GetByClaimantIdAsync(int claimantId, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                claim_id, case_id, claim_type_id, claimant_id, client_id,
                policy_number, delta_file_number, client_file_number,
                doctor_id, state_of_loss_id, alleged_injury, injury_description,
                attorney_representation, liability, workers_compensation, exposure
            FROM claims.claim 
            WHERE claimant_id = @ClaimantId";

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<dynamic>(sql, new { ClaimantId = claimantId });
        
        return results.Select(ClaimMapper.MapToClaim);
    }

    public async Task<IEnumerable<Claim>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                claim_id, case_id, claim_type_id, claimant_id, client_id,
                policy_number, delta_file_number, client_file_number,
                doctor_id, state_of_loss_id, alleged_injury, injury_description,
                attorney_representation, liability, workers_compensation, exposure
            FROM claims.claim 
            WHERE client_id = @ClientId";

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<dynamic>(sql, new { ClientId = clientId });
        
        return results.Select(ClaimMapper.MapToClaim);
    }

    public async Task<IEnumerable<Claim>> GetByPolicyNumberAsync(string policyNumber, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            SELECT 
                claim_id, case_id, claim_type_id, claimant_id, client_id,
                policy_number, delta_file_number, client_file_number,
                doctor_id, state_of_loss_id, alleged_injury, injury_description,
                attorney_representation, liability, workers_compensation, exposure
            FROM claims.claim 
            WHERE policy_number = @PolicyNumber";

        using var connection = _connectionFactory.CreateConnection();
        var results = await connection.QueryAsync<dynamic>(sql, new { PolicyNumber = policyNumber });
        
        return results.Select(ClaimMapper.MapToClaim);
    }

    public async Task<Claim> AddAsync(Claim claim, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            INSERT INTO claims.claim (
                claim_id, case_id, claim_type_id, claimant_id, client_id,
                policy_number, delta_file_number, client_file_number,
                doctor_id, state_of_loss_id, alleged_injury, injury_description,
                attorney_representation, liability, workers_compensation, exposure
            ) VALUES (
                @ClaimId, @CaseId, @ClaimTypeId, @ClaimantId, @ClientId,
                @PolicyNumber, @DeltaFileNumber, @ClientFileNumber,
                @DoctorId, @StateOfLossId, @AllegedInjury, @InjuryDescription,
                @AttorneyRepresentation, @Liability, @WorkersCompensation, @Exposure
            )";

        var parameters = new
        {
            ClaimId = claim.Id,
            CaseId = claim.CaseId,
            ClaimTypeId = claim.ClaimTypeId,
            ClaimantId = claim.ClaimantId,
            ClientId = claim.ClientId,
            PolicyNumber = claim.PolicyInfo.PolicyNumber,
            DeltaFileNumber = claim.PolicyInfo.DeltaFileNumber,
            ClientFileNumber = claim.PolicyInfo.ClientFileNumber,
            DoctorId = claim.DoctorId,
            StateOfLossId = claim.StateOfLossId,
            AllegedInjury = claim.InjuryInfo?.AllegedInjury,
            InjuryDescription = claim.InjuryInfo?.InjuryDescription,
            AttorneyRepresentation = claim.InjuryInfo?.AttorneyRepresentation ?? false,
            Liability = claim.InjuryInfo?.Liability,
            WorkersCompensation = claim.InjuryInfo?.WorkersCompensation ?? false,
            Exposure = claim.InjuryInfo?.Exposure
        };

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
        
        return claim;
    }

    public async Task UpdateAsync(Claim claim, CancellationToken cancellationToken = default)
    {
        const string sql = @"
            UPDATE claims.claim SET
                case_id = @CaseId,
                claim_type_id = @ClaimTypeId,
                claimant_id = @ClaimantId,
                client_id = @ClientId,
                policy_number = @PolicyNumber,
                delta_file_number = @DeltaFileNumber,
                client_file_number = @ClientFileNumber,
                doctor_id = @DoctorId,
                state_of_loss_id = @StateOfLossId,
                alleged_injury = @AllegedInjury,
                injury_description = @InjuryDescription,
                attorney_representation = @AttorneyRepresentation,
                liability = @Liability,
                workers_compensation = @WorkersCompensation,
                exposure = @Exposure
            WHERE claim_id = @ClaimId";

        var parameters = new
        {
            ClaimId = claim.Id,
            CaseId = claim.CaseId,
            ClaimTypeId = claim.ClaimTypeId,
            ClaimantId = claim.ClaimantId,
            ClientId = claim.ClientId,
            PolicyNumber = claim.PolicyInfo.PolicyNumber,
            DeltaFileNumber = claim.PolicyInfo.DeltaFileNumber,
            ClientFileNumber = claim.PolicyInfo.ClientFileNumber,
            DoctorId = claim.DoctorId,
            StateOfLossId = claim.StateOfLossId,
            AllegedInjury = claim.InjuryInfo?.AllegedInjury,
            InjuryDescription = claim.InjuryInfo?.InjuryDescription,
            AttorneyRepresentation = claim.InjuryInfo?.AttorneyRepresentation ?? false,
            Liability = claim.InjuryInfo?.Liability,
            WorkersCompensation = claim.InjuryInfo?.WorkersCompensation ?? false,
            Exposure = claim.InjuryInfo?.Exposure,
            ModifiedByUser = claim.ModifiedByUser,
            ModifiedOn = claim.ModifiedOn
        };

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, parameters);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = "DELETE FROM claims.claim WHERE claim_id = @Id";

        using var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        const string sql = "SELECT COUNT(1) FROM claims.claim WHERE claim_id = @Id";

        using var connection = _connectionFactory.CreateConnection();
        var count = await connection.QuerySingleAsync<int>(sql, new { Id = id });
        
        return count > 0;
    }
}
