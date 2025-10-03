namespace DeltaApi.Application.DTOs.Claims;

public class AddDocumentToClaimRequest
{
    public Guid ClaimId { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
}

