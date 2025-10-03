using DeltaApi.Domain.Claims;

namespace DeltaApi.Infrastructure.Mappings;

public static class ClaimTypeMapper
{
    public static ClaimType MapToClaimType(dynamic row)
    {
        return new ClaimType(
            id: row.ClaimTypeId,
            claimTypeDescription: row.ClaimTypeDescription,
            claimTypeShortCode: row.ClaimTypeShortCode,
            isActive: row.IsActive,
            createdByUser: row.CreatedByUser
        );
    }
}
