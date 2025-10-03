using DeltaApi.Domain.Claimants;

namespace DeltaApi.Domain.Events;

public sealed record ClaimantUpdatedEvent : IDomainEvent
{
    public ClaimantUpdatedEvent(Claimant claimant, string updatedField)
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        ClaimantId = claimant.Id;
        ClaimantName = claimant.PersonalInfo.FullName;
        UpdatedField = updatedField;
    }

    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public Guid ClaimantId { get; }
    public string ClaimantName { get; }
    public string UpdatedField { get; }
}
