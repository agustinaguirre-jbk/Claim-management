using DeltaApi.Domain.Claims;

namespace DeltaApi.Domain.Events;

public sealed record ClaimEventAddedEvent : IDomainEvent
{
    public ClaimEventAddedEvent(Claim claim, ClaimEvent claimEvent)
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        ClaimId = claim.Id;
        CaseId = claim.CaseId;
        EventId = claimEvent.Id;
        EventType = claimEvent.EventType;
        EventDate = claimEvent.EventDate;
    }

    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public Guid ClaimId { get; }
    public int CaseId { get; }
    public Guid EventId { get; }
    public string EventType { get; }
    public DateTime EventDate { get; }
}
