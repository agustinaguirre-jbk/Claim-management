using DeltaApi.Domain.Claims;

namespace DeltaApi.Domain.Repositories;

public interface IClaimTypeRepository
{
    Task<ClaimType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<ClaimType>> GetAllActiveAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<ClaimType>> SearchByDescriptionAsync(string description, CancellationToken cancellationToken = default);
    Task<ClaimType> AddAsync(ClaimType claimType, CancellationToken cancellationToken = default);
    Task UpdateAsync(ClaimType claimType, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
