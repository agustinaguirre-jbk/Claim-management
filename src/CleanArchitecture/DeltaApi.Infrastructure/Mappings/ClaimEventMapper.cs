using DeltaApi.Domain.Claims;

namespace DeltaApi.Infrastructure.Mappings;

public static class ClaimEventMapper
{
    public static ClaimEvent MapToClaimEvent(dynamic row)
    {
        return new ClaimEvent(
            id: row.ClaimEventId,
            claimId: row.ClaimId,
            eventType: row.EventType,
            eventDate: row.EventDate,
            notes: row.Notes,
            createdByUser: row.CreatedByUser
        );
    }
}
