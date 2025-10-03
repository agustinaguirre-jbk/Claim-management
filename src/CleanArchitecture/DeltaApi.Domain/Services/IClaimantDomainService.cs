using DeltaApi.Domain.Claimants;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Domain.Services;

public interface IClaimantDomainService
{
    Task<bool> IsEmailUniqueAsync(Email email, Guid? excludeClaimantId = null, CancellationToken cancellationToken = default);
    Task<bool> IsSSNUniqueAsync(string ssn, Guid? excludeClaimantId = null, CancellationToken cancellationToken = default);
    Task<bool> CanCreateChildClaimantAsync(Guid parentClaimantId, CancellationToken cancellationToken = default);
    Task<Claimant> CreateChildClaimantAsync(Claimant parentClaimant, PersonalInfo personalInfo, CancellationToken cancellationToken = default);
}
