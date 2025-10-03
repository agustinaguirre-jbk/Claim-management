using DeltaApi.Application.DTOs.Claims;
using DeltaApi.Application.Interfaces.Claims;
using DeltaApi.Domain.Repositories;

namespace DeltaApi.Application.UseCases.Claims;

public class GetClaimsByClaimantIdUseCase : IGetClaimsByClaimantIdUseCase
{
    private readonly IClaimRepository _claimRepository;

    public GetClaimsByClaimantIdUseCase(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
    }

    public async Task<IEnumerable<ClaimResponse>> ExecuteAsync(int claimantId, CancellationToken cancellationToken = default)
    {
        var claims = await _claimRepository.GetByClaimantIdAsync(claimantId, cancellationToken);
        
        return claims.Select(MapToResponse);
    }

    private static ClaimResponse MapToResponse(Domain.Claims.Claim claim)
    {
        return new ClaimResponse
        {
            Id = claim.Id,
            CaseId = claim.CaseId,
            ClaimTypeId = claim.ClaimTypeId,
            ClaimantId = claim.ClaimantId,
            ClientId = claim.ClientId,
            PolicyNumber = claim.PolicyInfo.PolicyNumber,
            DeltaFileNumber = claim.PolicyInfo.DeltaFileNumber,
            ClientFileNumber = claim.PolicyInfo.ClientFileNumber,
            DoctorId = claim.DoctorId,
            StateOfLossId = claim.StateOfLossId,
            AllegedInjury = claim.InjuryInfo?.AllegedInjury,
            InjuryDescription = claim.InjuryInfo?.InjuryDescription,
            AttorneyRepresentation = claim.InjuryInfo?.AttorneyRepresentation ?? false,
            Liability = claim.InjuryInfo?.Liability,
            WorkersCompensation = claim.InjuryInfo?.WorkersCompensation ?? false,
            Exposure = claim.InjuryInfo?.Exposure,
            CreatedOn = claim.CreatedOn,
            ModifiedOn = claim.ModifiedOn,
            Documents = claim.Documents.Select(d => new ClaimDocumentResponse
            {
                Id = d.Id,
                ClaimId = d.ClaimId,
                DocumentType = d.DocumentType,
                FilePath = d.FilePath,
                CreatedOn = d.CreatedOn
            }).ToList(),
            Events = claim.Events.Select(e => new ClaimEventResponse
            {
                Id = e.Id,
                ClaimId = e.ClaimId,
                EventType = e.EventType,
                EventDate = e.EventDate,
                Notes = e.Notes,
                CreatedOn = e.CreatedOn
            }).ToList()
        };
    }
}

