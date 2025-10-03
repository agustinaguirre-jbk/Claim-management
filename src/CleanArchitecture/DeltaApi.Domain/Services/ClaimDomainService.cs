using DeltaApi.Domain.Claims;
using DeltaApi.Domain.Repositories;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Domain.Services;

public class ClaimDomainService : IClaimDomainService
{
    private readonly IClaimRepository _claimRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IStateOfLossRepository _stateOfLossRepository;

    public ClaimDomainService(
        IClaimRepository claimRepository,
        IDoctorRepository doctorRepository,
        IStateOfLossRepository stateOfLossRepository)
    {
        _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
        _doctorRepository = doctorRepository ?? throw new ArgumentNullException(nameof(doctorRepository));
        _stateOfLossRepository = stateOfLossRepository ?? throw new ArgumentNullException(nameof(stateOfLossRepository));
    }

    public async Task<bool> IsPolicyNumberUniqueAsync(string policyNumber, Guid? excludeClaimId = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(policyNumber))
            return true;

        var existingClaims = await _claimRepository.GetByPolicyNumberAsync(policyNumber, cancellationToken);
        
        if (excludeClaimId.HasValue)
        {
            return !existingClaims.Any(c => c.Id != excludeClaimId.Value);
        }

        return !existingClaims.Any();
    }

    public Task<bool> CanCreateClaimAsync(int claimantId, int clientId, CancellationToken cancellationToken = default)
    {
        // Business rules for claim creation
        // For example: check if claimant exists, if client is active, etc.
        return Task.FromResult(true);
    }

    public async Task<Claim> CreateClaimAsync(int caseId, int claimTypeId, int claimantId, int clientId, string policyNumber, CancellationToken cancellationToken = default)
    {
        // Validate business rules
        if (!await CanCreateClaimAsync(claimantId, clientId, cancellationToken))
        {
            throw new InvalidOperationException("No se puede crear la reclamación con los parámetros proporcionados");
        }

        if (!await IsPolicyNumberUniqueAsync(policyNumber, null, cancellationToken))
        {
            throw new InvalidOperationException("El número de póliza ya existe");
        }

        var policyInfo = new PolicyInfo(policyNumber);
        
        var claim = new Claim(
            id: Guid.NewGuid(),
            caseId: caseId,
            claimTypeId: claimTypeId,
            claimantId: claimantId,
            clientId: clientId,
            policyInfo: policyInfo,
            createdByUser: 1 // This should come from the current user context
        );

        return claim;
    }

    public async Task<bool> CanAssignDoctorAsync(int doctorId, Guid claimId, CancellationToken cancellationToken = default)
    {
        var doctor = await _doctorRepository.GetByIdAsync(new Guid(doctorId.ToString()), cancellationToken);
        return doctor != null;
    }

    public async Task<bool> CanAssignStateOfLossAsync(int stateOfLossId, Guid claimId, CancellationToken cancellationToken = default)
    {
        var stateOfLoss = await _stateOfLossRepository.GetByIdAsync(new Guid(stateOfLossId.ToString()), cancellationToken);
        return stateOfLoss != null;
    }
}
