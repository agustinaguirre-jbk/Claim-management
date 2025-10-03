using DeltaApi.Domain.Abstractions;

namespace DeltaApi.Domain.Claimants;

public sealed class ClaimantSocialMedia : Entity
{
    public ClaimantSocialMedia(
        Guid id,
        Guid claimantId,
        string platform,
        string? url = null,
        bool hasPresence = false,
        string? details = null,
        int? caseId = null) : base(id)
    {
        ClaimantId = claimantId;
        Platform = platform ?? throw new ArgumentNullException(nameof(platform));
        Url = url;
        HasPresence = hasPresence;
        Details = details;
        CaseId = caseId;
    }

    public Guid ClaimantId { get; private set; }
    public int? CaseId { get; private set; }
    public string Platform { get; private set; }
    public string? Url { get; private set; }
    public bool HasPresence { get; private set; }
    public string? Details { get; private set; }

    public void UpdatePresence(bool hasPresence, string? url = null, string? details = null)
    {
        HasPresence = hasPresence;
        Url = url;
        Details = details;
    }
}
