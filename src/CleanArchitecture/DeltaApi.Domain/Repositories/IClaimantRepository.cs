using DeltaApi.Domain.Claimants;

namespace DeltaApi.Domain.Repositories;

public interface IClaimantRepository
{
    Task<Claimant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Claimant?> GetByTokenIdAsync(int tokenId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Claimant>> GetByParentClaimantIdAsync(Guid parentClaimantId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Claimant>> SearchByNameAsync(string firstName, string? lastName = null, CancellationToken cancellationToken = default);
    Task<IEnumerable<Claimant>> SearchBySSNAsync(string ssn, CancellationToken cancellationToken = default);
    Task<Claimant> AddAsync(Claimant claimant, CancellationToken cancellationToken = default);
    Task UpdateAsync(Claimant claimant, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
