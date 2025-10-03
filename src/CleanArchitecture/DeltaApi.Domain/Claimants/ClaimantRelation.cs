using DeltaApi.Domain.Abstractions;

namespace DeltaApi.Domain.Claimants;

public sealed class ClaimantRelation : Entity
{
    public ClaimantRelation(
        Guid id,
        Guid claimantId,
        string relationshipType,
        Guid? relatedClaimantId = null,
        string? details = null) : base(id)
    {
        ClaimantId = claimantId;
        RelationshipType = relationshipType ?? throw new ArgumentNullException(nameof(relationshipType));
        RelatedClaimantId = relatedClaimantId;
        Details = details;
    }

    public Guid ClaimantId { get; private set; }
    public string RelationshipType { get; private set; }
    public Guid? RelatedClaimantId { get; private set; }
    public string? Details { get; private set; }

    public void UpdateRelationship(string relationshipType, Guid? relatedClaimantId = null, string? details = null)
    {
        RelationshipType = relationshipType ?? throw new ArgumentNullException(nameof(relationshipType));
        RelatedClaimantId = relatedClaimantId;
        Details = details;
    }
}
