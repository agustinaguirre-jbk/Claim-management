using DeltaApi.Domain.Claims;

namespace DeltaApi.Domain.Repositories;

public interface IClaimRepository
{
    Task<Claim?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Claim?> GetByCaseIdAsync(int caseId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Claim>> GetByClaimantIdAsync(int claimantId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Claim>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Claim>> GetByPolicyNumberAsync(string policyNumber, CancellationToken cancellationToken = default);
    Task<Claim> AddAsync(Claim claim, CancellationToken cancellationToken = default);
    Task UpdateAsync(Claim claim, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
