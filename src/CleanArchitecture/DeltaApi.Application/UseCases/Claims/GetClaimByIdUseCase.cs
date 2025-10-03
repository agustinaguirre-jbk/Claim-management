using DeltaApi.Application.DTOs.Claims;
using DeltaApi.Application.Interfaces.Claims;
using DeltaApi.Domain.Repositories;

namespace DeltaApi.Application.UseCases.Claims;

public class GetClaimByIdUseCase : IGetClaimByIdUseCase
{
    private readonly IClaimRepository _claimRepository;

    public GetClaimByIdUseCase(IClaimRepository claimRepository)
    {
        _claimRepository = claimRepository ?? throw new ArgumentNullException(nameof(claimRepository));
    }

    public async Task<ClaimResponse?> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var claim = await _claimRepository.GetByIdAsync(id, cancellationToken);
        
        if (claim == null)
            return null;

        return MapToResponse(claim);
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
