using DeltaApi.Domain.Claims;

namespace DeltaApi.Domain.Events;

public sealed record ClaimDocumentAddedEvent : IDomainEvent
{
    public ClaimDocumentAddedEvent(Claim claim, ClaimDocument document)
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTime.UtcNow;
        ClaimId = claim.Id;
        CaseId = claim.CaseId;
        DocumentId = document.Id;
        DocumentType = document.DocumentType;
        FilePath = document.FilePath;
    }

    public Guid Id { get; }
    public DateTime OccurredOn { get; }
    public Guid ClaimId { get; }
    public int CaseId { get; }
    public Guid DocumentId { get; }
    public string DocumentType { get; }
    public string FilePath { get; }
}
