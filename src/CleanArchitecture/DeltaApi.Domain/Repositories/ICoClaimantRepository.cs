using DeltaApi.Domain.Claimants;

namespace DeltaApi.Domain.Repositories;

public interface ICoClaimantRepository
{
    Task<CoClaimant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CoClaimant>> GetByCaseIdAsync(int caseId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CoClaimant>> GetByClaimantIdAsync(Guid claimantId, CancellationToken cancellationToken = default);
    Task<CoClaimant> AddAsync(CoClaimant coClaimant, CancellationToken cancellationToken = default);
    Task UpdateAsync(CoClaimant coClaimant, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
