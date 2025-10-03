using DeltaApi.Domain.Abstractions;
using DeltaApi.Domain.ValueObjects;

namespace DeltaApi.Domain.Claimants;

public sealed class ClaimantAddress : Entity
{
    public ClaimantAddress(
        Guid id, 
        Guid claimantId, 
        Address address, 
        AddressType addressType, 
        bool isPrimary = false,
        string? notes = null) : base(id)
    {
        ClaimantId = claimantId;
        Address = address ?? throw new ArgumentNullException(nameof(address));
        AddressType = addressType;
        IsPrimary = isPrimary;
        Notes = notes;
    }

    public Guid ClaimantId { get; private set; }
    public Address Address { get; private set; }
    public AddressType AddressType { get; private set; }
    public bool IsPrimary { get; private set; }
    public string? Notes { get; private set; }

    public void SetPrimary(bool isPrimary)
    {
        IsPrimary = isPrimary;
    }

    public void UpdateAddress(Address address)
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }
}

public enum AddressType
{
    Home,
    Work,
    Mailing,
    Other
}
