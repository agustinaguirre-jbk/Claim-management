using DeltaApi.Domain.Claimants;
using DeltaApi.Domain.Repositories;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Domain.Services;

public class ClaimantDomainService : IClaimantDomainService
{
    private readonly IClaimantRepository _claimantRepository;

    public ClaimantDomainService(IClaimantRepository claimantRepository)
    {
        _claimantRepository = claimantRepository ?? throw new ArgumentNullException(nameof(claimantRepository));
    }

    public Task<bool> IsEmailUniqueAsync(Email email, Guid? excludeClaimantId = null, CancellationToken cancellationToken = default)
    {
        // This would need to be implemented based on your specific business rules
        // For now, returning true as a placeholder
        return Task.FromResult(true);
    }

    public async Task<bool> IsSSNUniqueAsync(string ssn, Guid? excludeClaimantId = null, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(ssn))
            return true;

        var existingClaimants = await _claimantRepository.SearchBySSNAsync(ssn, cancellationToken);
        
        if (excludeClaimantId.HasValue)
        {
            return !existingClaimants.Any(c => c.Id != excludeClaimantId.Value);
        }

        return !existingClaimants.Any();
    }

    public async Task<bool> CanCreateChildClaimantAsync(Guid parentClaimantId, CancellationToken cancellationToken = default)
    {
        var parentClaimant = await _claimantRepository.GetByIdAsync(parentClaimantId, cancellationToken);
        
        if (parentClaimant == null)
            return false;

        // Business rule: Check if parent claimant can have children
        // This could include checking if they're not already a child, etc.
        return true;
    }

    public async Task<Claimant> CreateChildClaimantAsync(Claimant parentClaimant, PersonalInfo personalInfo, CancellationToken cancellationToken = default)
    {
        if (parentClaimant == null)
            throw new ArgumentNullException(nameof(parentClaimant));

        if (personalInfo == null)
            throw new ArgumentNullException(nameof(personalInfo));

        var childClaimant = new Claimant(
            id: Guid.NewGuid(),
            personalInfo: personalInfo,
            parentClaimantId: parentClaimant.Id,
            tokenId: null // Child claimants might not have token IDs initially
        );

        return childClaimant;
    }
}
