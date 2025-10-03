using DeltaApi.Domain.Claims;

namespace DeltaApi.Domain.Events;

public sealed record ClaimUpdatedEvent : IDomainEvent
{
    public ClaimUpdatedEvent(Claim claim, string updatedField)
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        ClaimId = claim.Id;
        CaseId = claim.CaseId;
        PolicyNumber = claim.PolicyInfo.PolicyNumber;
        UpdatedField = updatedField;
    }

    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public Guid ClaimId { get; }
    public int CaseId { get; }
    public string PolicyNumber { get; }
    public string UpdatedField { get; }
}
