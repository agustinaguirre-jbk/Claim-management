using DeltaApi.Domain.Abstractions;

namespace DeltaApi.Domain.Claims;

public sealed class ClaimEvent : Entity
{
    public ClaimEvent(
        Guid id,
        Guid claimId,
        string eventType,
        DateTime eventDate,
        string? notes = null,
        int createdByUser = 1) : base(id, createdByUser)
    {
        ClaimId = claimId;
        EventType = eventType ?? throw new ArgumentNullException(nameof(eventType));
        EventDate = eventDate;
        Notes = notes;
    }

    public Guid ClaimId { get; private set; }
    public string EventType { get; private set; }
    public DateTime EventDate { get; private set; }
    public string? Notes { get; private set; }

    public void UpdateEvent(string eventType, DateTime eventDate, string? notes, int modifiedByUser)
    {
        EventType = eventType ?? throw new ArgumentNullException(nameof(eventType));
        EventDate = eventDate;
        Notes = notes;
        MarkAsModified(modifiedByUser);
    }
}
