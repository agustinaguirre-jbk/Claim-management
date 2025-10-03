using DeltaApi.Domain.Abstractions;

namespace DeltaApi.Domain.Claims;

public sealed class ClaimType : Entity
{
    public ClaimType(
        Guid id,
        string claimTypeDescription,
        string? claimTypeShortCode = null,
        bool isActive = true,
        int createdByUser = 1) : base(id, createdByUser)
    {
        ClaimTypeDescription = claimTypeDescription ?? throw new ArgumentNullException(nameof(claimTypeDescription));
        ClaimTypeShortCode = claimTypeShortCode;
        IsActive = isActive;
    }

    public string ClaimTypeDescription { get; private set; }
    public string? ClaimTypeShortCode { get; private set; }
    public bool IsActive { get; private set; }

    public void UpdateClaimType(string description, string? shortCode, int modifiedByUser)
    {
        ClaimTypeDescription = description ?? throw new ArgumentNullException(nameof(description));
        ClaimTypeShortCode = shortCode;
        MarkAsModified(modifiedByUser);
    }

    public void Activate(int modifiedByUser)
    {
        IsActive = true;
        MarkAsModified(modifiedByUser);
    }

    public void Deactivate(int modifiedByUser)
    {
        IsActive = false;
        MarkAsModified(modifiedByUser);
    }
}
