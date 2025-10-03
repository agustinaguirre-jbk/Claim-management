using DeltaApi.Domain.Claims;

namespace DeltaApi.Domain.Events;

public sealed record ClaimCreatedEvent : IDomainEvent
{
    public ClaimCreatedEvent(Claim claim)
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        ClaimId = claim.Id;
        CaseId = claim.CaseId;
        PolicyNumber = claim.PolicyInfo.PolicyNumber;
        ClaimantId = claim.ClaimantId;
        ClientId = claim.ClientId;
    }

    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public Guid ClaimId { get; }
    public int CaseId { get; }
    public string PolicyNumber { get; }
    public int ClaimantId { get; }
    public int ClientId { get; }
}
