namespace DeltaApi.Application.DTOs.Claims;

public class AddEventToClaimRequest
{
    public Guid ClaimId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string? Notes { get; set; }
}

