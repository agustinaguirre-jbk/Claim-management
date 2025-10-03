using DeltaApi.Domain.Abstractions;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Domain.Claimants;

public sealed class ClaimantEmail : Entity
{
    public ClaimantEmail(Guid id, Guid claimantId, Email emailAddress) : base(id)
    {
        ClaimantId = claimantId;
        EmailAddress = emailAddress;
    }

    public Guid ClaimantId { get; private set; }
    public Email EmailAddress { get; private set; }
}
