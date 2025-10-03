namespace DeltaApi.Application.DTOs.Claims;

public class CreateClaimRequest
{
    public int CaseId { get; set; }
    public int ClaimTypeId { get; set; }
    public int ClaimantId { get; set; }
    public int ClientId { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public string? DeltaFileNumber { get; set; }
    public string? ClientFileNumber { get; set; }
    public int? DoctorId { get; set; }
    public int? StateOfLossId { get; set; }
    public string? AllegedInjury { get; set; }
    public string? InjuryDescription { get; set; }
    public bool AttorneyRepresentation { get; set; }
    public string? Liability { get; set; }
    public bool WorkersCompensation { get; set; }
    public decimal? Exposure { get; set; }
}

