using DeltaApi.Domain.Abstractions;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Domain.Claimants;

public sealed class ClaimantPhone : Entity
{
    public ClaimantPhone(Guid id, Guid claimantId, PhoneNumber phoneNumber) : base(id)
    {
        ClaimantId = claimantId;
        PhoneNumber = phoneNumber;
    }

    public Guid ClaimantId { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
}
