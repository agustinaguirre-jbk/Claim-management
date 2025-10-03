using DeltaApi.Domain.Claims;

namespace DeltaApi.Infrastructure.Mappings;

public static class ClaimDocumentMapper
{
    public static ClaimDocument MapToClaimDocument(dynamic row)
    {
        return new ClaimDocument(
            id: row.ClaimDocumentId,
            claimId: row.ClaimId,
            documentType: row.DocumentType,
            filePath: row.FilePath,
            createdByUser: row.CreatedByUser
        );
    }
}
