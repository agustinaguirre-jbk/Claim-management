namespace DeltaApi.Application.DTOs.Claims;

public class ClaimResponse
{
    public Guid Id { get; set; }
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
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public List<ClaimDocumentResponse> Documents { get; set; } = new();
    public List<ClaimEventResponse> Events { get; set; } = new();
}

public class ClaimDocumentResponse
{
    public Guid Id { get; set; }
    public Guid ClaimId { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
}

public class ClaimEventResponse
{
    public Guid Id { get; set; }
    public Guid ClaimId { get; set; }
    public string EventType { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedOn { get; set; }
}
