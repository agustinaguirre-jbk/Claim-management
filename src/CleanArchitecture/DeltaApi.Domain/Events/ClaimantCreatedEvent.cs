using DeltaApi.Domain.Claimants;

namespace DeltaApi.Domain.Events;

public sealed record ClaimantCreatedEvent : IDomainEvent
{
    public ClaimantCreatedEvent(Claimant claimant)
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        ClaimantId = claimant.Id;
        ClaimantName = claimant.PersonalInfo.FullName;
    }

    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public Guid ClaimantId { get; }
    public string ClaimantName { get; }
}
