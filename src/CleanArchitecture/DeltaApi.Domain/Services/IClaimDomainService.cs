using DeltaApi.Domain.Claims;

namespace DeltaApi.Domain.Services;

public interface IClaimDomainService
{
    Task<bool> IsPolicyNumberUniqueAsync(string policyNumber, Guid? excludeClaimId = null, CancellationToken cancellationToken = default);
    Task<bool> CanCreateClaimAsync(int claimantId, int clientId, CancellationToken cancellationToken = default);
    Task<Claim> CreateClaimAsync(int caseId, int claimTypeId, int claimantId, int clientId, string policyNumber, CancellationToken cancellationToken = default);
    Task<bool> CanAssignDoctorAsync(int doctorId, Guid claimId, CancellationToken cancellationToken = default);
    Task<bool> CanAssignStateOfLossAsync(int stateOfLossId, Guid claimId, CancellationToken cancellationToken = default);
}
